using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class Factory
    {
        public Factory()
        {
            FactoryUnit = new HashSet<FactoryUnit>();
        }

        public long Id { get; set; }
        public string Address { get; set; }

        public virtual ICollection<FactoryUnit> FactoryUnit { get; set; }
    }
}
