using System;
using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class Ingredient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long MeasurementUnitId { get; set; }

        public virtual MeasurementUnit MeasurementUnit { get; set; }
    }
}
