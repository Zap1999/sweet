using System;
using System.Collections.Generic;

namespace Sweet.Models
{
    public partial class User
    {
        public User()
        {
            FactoryUnit = new HashSet<FactoryUnit>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<FactoryUnit> FactoryUnit { get; set; }
    }
}
