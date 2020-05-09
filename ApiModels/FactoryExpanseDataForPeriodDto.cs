using System.Collections.Generic;
using SweetLife.Models;

namespace Sweets.ApiModels
{
    public class FactoryExpanseDataForPeriodDto
    {
        public IEnumerable<ManufacturingOrderItem> ManufacturingOrderItems { get; set; }
        
        public IEnumerable<SweetIngredient> SweetIngredients { get; set; }
    }
}