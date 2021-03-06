﻿using System;

namespace SweetLife.Models
{
    public partial class ManufacturingOrder
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long StatusId { get; set; }
        public long FactoryUnitId { get; set; }
        public DateTime DeadlineDate { get; set; }

        public virtual FactoryUnit FactoryUnit { get; set; }
        public virtual ManufacturingOrderStatus Status { get; set; }
    }
}
