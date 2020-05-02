using System;
using System.Collections.Generic;

namespace Sweet.Models
{
    public partial class FactoryUnit
    {
        public FactoryUnit()
        {
            ManufacturingOrder = new HashSet<ManufacturingOrder>();
        }

        public long Id { get; set; }
        public long ControllerId { get; set; }
        public long CategoryId { get; set; }
        public long FactoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User Controller { get; set; }
        public virtual Factory Factory { get; set; }
        public virtual ICollection<ManufacturingOrder> ManufacturingOrder { get; set; }
    }
}
