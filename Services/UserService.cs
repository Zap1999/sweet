using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using Sweets.ApiModels;


namespace Sweets.Services
{
    public class UserService
    {
        private const string RegisterSqlCommand = "INSERT INTO [dbo].[user] ([first_name],[last_name],[email],[password],[role_id]) VALUES (@first_name, @last_name, @email, @password, @role_id)";

        private readonly SweetLifeDbContext _context;


        public UserService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Register(User newUser)
        {
            var firstName = new SqlParameter("first_name", newUser.FirstName);
            var lastName = new SqlParameter("last_name", newUser.LastName);
            var email = new SqlParameter("email", newUser.Email);
            var password = new SqlParameter("password", PasswordHasher.Hash(newUser.Password));
            var roleId = new SqlParameter("role_id", newUser.RoleId);

            _context.Database.ExecuteSqlCommand(RegisterSqlCommand, firstName, lastName, email, password, roleId);
            _context.SaveChanges();
        }

        public User LogIn(LogInForm logInForm)
        {
            var user = _context.User.FromSqlRaw($"SELECT * FROM [dbo].[user] WHERE [user].email = '{logInForm.Email}'").FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var role = _context.Role.FromSqlRaw($"SELECT * FROM role WHERE role.id = {user.RoleId}").First();
            user.Role = role;

            return PasswordHasher.Check(user.Password, logInForm.Password).Verified ? user : null;
        }


        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private sealed class PasswordHasher
        {
            private const int SaltSize = 16; // 128 bit 
            private const int KeySize = 32; // 256 bit

            public static string Hash(string password)
            {
                using var algorithm = new Rfc2898DeriveBytes(
                  password,
                  SaltSize,
                  10000,
                  HashAlgorithmName.SHA512);
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{10000}.{salt}.{key}";
            }

            public static (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
            {
                var parts = hash.Split('.', 3);

                if (parts.Length != 3)
                {
                    throw new FormatException("Unexpected hash format. " +
                      "Should be formatted as `{iterations}.{salt}.{hash}`");
                }

                var iterations = Convert.ToInt32(parts[0]);
                var salt = Convert.FromBase64String(parts[1]);
                var key = Convert.FromBase64String(parts[2]);

                var needsUpgrade = iterations != 10000;

                using var algorithm = new Rfc2898DeriveBytes(
                  password,
                  salt,
                  iterations,
                  HashAlgorithmName.SHA512);
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return (verified, needsUpgrade);
            }
        }

        public IEnumerable<User> GetUsersByRoleId(int roleId)
        {
            var userList = _context.User.FromSqlRaw($"dbo.GetUsersByRoleId {roleId}").ToList();
            userList.ForEach(u => u.FactoryUnit = null);

            return userList;
        }
    }
}
