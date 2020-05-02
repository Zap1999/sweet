using System;
using System.Collections.Generic;

namespace Sweet.Models
{
    public partial class IngredientStorage
    {
        public long IngredientId { get; set; }
        public long FactoryId { get; set; }
        public long Count { get; set; }

        public virtual Factory Factory { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
