using System;
using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class ManufacturingOrderPostDto
    {
        public DateTime DeadLineDate { get; set; }
        
        public long FactoryUnitId { get; set; }
        
        public long StatusId { get; set; }

        public IEnumerable<ManufacturingOrderItem> ManufacturingOrderItems { get; set; }
    }
}