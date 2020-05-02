using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SweetLife.Models;
using Sweets.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Sweets.Services
{
    public class UserService
    {
        private readonly SweetLifeDbContext _context;
        private readonly string REGISTER_SQL_COMMAND = "INSERT INTO [dbo].[user] ([first_name],[last_name],[email],[password],[role_id])" 
            + "VALUES (@first_name, @last_name, @email, @password, @role_id)";
        private readonly string LOGIN_SQL_COMMAND = "SELECT * FROM [dbo].[user] WHERE [user].email = '@email'";


        public UserService(SweetLifeDbContext context)
        {
            _context = context;
        }

        public void Register(User newUser)
        {
            var firstName = new SqlParameter("first_name", newUser.FirstName);
            var lastName = new SqlParameter("last_name", newUser.LastName);
            var email = new SqlParameter("email", newUser.Email);
            var password = new SqlParameter("password", newUser.Password);
            var roleId = new SqlParameter("role_id", 1);

            _context.Database.ExecuteSqlCommand(REGISTER_SQL_COMMAND, firstName, lastName, email, password, roleId);
            _context.SaveChanges();
        }

        public User LogIn(LogInForm logInForm)
        {
            var email = new SqlParameter("email", logInForm.Email);
            var user = _context.User.FromSqlRaw(LOGIN_SQL_COMMAND, email).FirstOrDefault();

            return user == null ? user : user.Password.Equals(logInForm.Password) ? user : null;
        }
    }
}
