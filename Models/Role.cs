using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Salary { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
