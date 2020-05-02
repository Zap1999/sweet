using System;
using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class ManufacturingOrderStatus
    {
        public ManufacturingOrderStatus()
        {
            ManufacturingOrder = new HashSet<ManufacturingOrder>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ManufacturingOrder> ManufacturingOrder { get; set; }
    }
}
