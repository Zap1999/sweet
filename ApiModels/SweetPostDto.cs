using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class SweetPostDto
    {
        public Sweet Sweet { get; set; }
        
        public IEnumerable<SweetIngredient> SweetIngredients { get; set; }
    }
}