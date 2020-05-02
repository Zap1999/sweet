using System;
using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class UnitWorker
    {
        public long UnitId { get; set; }
        public long WorkerId { get; set; }

        public virtual FactoryUnit Unit { get; set; }
        public virtual User Worker { get; set; }
    }
}
