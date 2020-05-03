using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.HelperModels;
using System;
using System.Linq;
using System.Security.Cryptography;


namespace Sweets.Services
{
    public class UserService
    {
        private readonly string REGISTER_SQL_COMMAND = "INSERT INTO [dbo].[user] ([first_name],[last_name],[email],[password],[role_id])"
            + "VALUES (@first_name, @last_name, @email, @password, @role_id)";

        private readonly PasswordHasher passwordHasher;
        private readonly SweetLifeDbContext context;


        public UserService(SweetLifeDbContext context)
        {
            this.context = context;
            passwordHasher = new PasswordHasher();
        }

        public void Register(User newUser)
        {
            var firstName = new SqlParameter("first_name", newUser.FirstName);
            var lastName = new SqlParameter("last_name", newUser.LastName);
            var email = new SqlParameter("email", newUser.Email);
            var password = new SqlParameter("password", passwordHasher.Hash(newUser.Password));
            var roleId = new SqlParameter("role_id", 1);

            #pragma warning disable CS0618 // Type or member is obsolete
            context.Database.ExecuteSqlCommand(REGISTER_SQL_COMMAND, firstName, lastName, email, password, roleId);
            #pragma warning restore CS0618 // Type or member is obsolete

            context.SaveChanges();
        }

        public User LogIn(LogInForm logInForm)
        {
            var user = context.User.FromSqlRaw($"SELECT * FROM [dbo].[user] WHERE [user].email = '{logInForm.Email}'").FirstOrDefault();

            return user == null ? user : passwordHasher.Check(user.Password, logInForm.Password).Verified ? user : null;
        }

        public sealed class PasswordHasher
        {
            private const int SaltSize = 16; // 128 bit 
            private const int KeySize = 32; // 256 bit

            public string Hash(string password)
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

            public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
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
    }
}
