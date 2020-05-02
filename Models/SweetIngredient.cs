using System;
using System.Collections.Generic;

namespace SweetLife.Models
{
    public partial class SweetIngredient
    {
        public long SweetId { get; set; }
        public long IngredientId { get; set; }
        public long Count { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Sweet Sweet { get; set; }
    }
}
