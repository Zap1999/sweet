using System.Collections;
using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class IngredientsExpanseDataDto
    {
        public IEnumerable<Ingredient> Ingredients { get; set; }
        
        public Dictionary<string, decimal> Counts { get; set; }
    }
}