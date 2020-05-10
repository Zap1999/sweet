using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/ingredient_storage")]
    [ApiController]
    public class IngredientStorageController : ControllerBase
    {
        private readonly IngredientStorageService _service;

        
        public IngredientStorageController()
        {
            _service = new IngredientStorageService(new SweetLifeDbContext());
        }

        [HttpGet("{factoryId}")]
        public IEnumerable<IngredientStorage> GetByFactoryId([FromRoute] long factoryId)
        {
            return _service.GetByFactoryId(factoryId);
        }

        [HttpPut("{ingredientId}/{factoryId}")]
        public void UpdateIngredientStorage([FromRoute] int ingredientId, [FromRoute] int factoryId, [FromBody] decimal count)
        {
            _service.UpdateIngredientStorage(ingredientId, factoryId, count);
        }
    }
}