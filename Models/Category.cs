using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class Category
    {
        public Category()
        {
            FactoryUnit = new HashSet<FactoryUnit>();
            Sweet = new HashSet<Sweet>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FactoryUnit> FactoryUnit { get; set; }
        public virtual ICollection<Sweet> Sweet { get; set; }
    }
}
