using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IngredientService _service;

        
        public IngredientController()
        {
            _service = new IngredientService(new SweetLifeDbContext());
        }

        [HttpPost]
        public void Post([FromBody] Ingredient ingredient)
        {
            _service.Save(ingredient);
        }

        [HttpPut("{ingredientId}/{factoryId}")]
        public void UpdateIngredientStorage([FromRoute] int ingredientId, [FromRoute] int factoryId, [FromBody] decimal count)
        {
            _service.UpdateIngredientStorage(ingredientId, factoryId, count);
        }
    }
}