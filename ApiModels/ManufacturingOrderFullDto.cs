using System.Collections;
using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class ManufacturingOrderFullDto
    {
        public ManufacturingOrder ManufacturingOrder { get; set; }

        public IEnumerable<ManufacturingOrderItem> ManufacturingOrderItems { get; set; }

        public IEnumerable<SweetIngredient> SweetsIngredients { get; set; }
    }
}