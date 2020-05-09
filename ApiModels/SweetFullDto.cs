using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class SweetFullDto
    {
        public Sweet Sweet { get; set; }
        
        public IEnumerable<SweetIngredient> SweetIngredients { get; set; }
    }
}