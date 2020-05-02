﻿using System;
using System.Collections.Generic;

namespace SweetLife.Models
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
