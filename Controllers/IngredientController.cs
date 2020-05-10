using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.ApiModels;
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

        [HttpGet]
        public IEnumerable<Ingredient> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("expanse/{startDate}/{endDate}")]
        public IngredientsExpanseDataDto GetIngredientsExpanseData([FromRoute] string startDate,
            [FromRoute] string endDate)
        {
            return _service.GetAllIngredientsExpanseDataForPeriod(
                DateTime.ParseExact(startDate, "yyyyMMdd",
                    CultureInfo.InvariantCulture),
                DateTime.ParseExact(endDate, "yyyyMMdd",
                    CultureInfo.InvariantCulture)
            );
        }

        [HttpGet("expanse/{factoryId}/{startDate}/{endDate}")]
        public IngredientsExpanseDataDto GetIngredientsExpanseData([FromRoute] long factoryId,
            [FromRoute] string startDate,
            [FromRoute] string endDate)
        {
            return _service.GetAllIngredientsExpanseDataForFactoryAndPeriod(
                factoryId,
                DateTime.ParseExact(startDate, "yyyyMMdd",
                    CultureInfo.InvariantCulture),
                DateTime.ParseExact(endDate, "yyyyMMdd",
                    CultureInfo.InvariantCulture)
            );
        }

        [HttpPost]
        public void Post([FromBody] Ingredient ingredient)
        {
            _service.Save(ingredient);
        }

        [HttpPut("{ingredientId}/{factoryId}")]
        public void UpdateIngredientStorage([FromRoute] int ingredientId, [FromRoute] int factoryId,
            [FromBody] decimal count)
        {
            _service.UpdateIngredientStorage(ingredientId, factoryId, count);
        }
    }
}