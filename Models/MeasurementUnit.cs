using System;
using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class MeasurementUnit
    {
        public MeasurementUnit()
        {
            Ingredient = new HashSet<Ingredient>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}
