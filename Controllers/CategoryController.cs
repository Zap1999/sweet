using Microsoft.AspNetCore.Mvc;
using SweetLife.Models;
using Sweets.Services;

namespace Sweets.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;


        public CategoryController()
        {
            _service = new CategoryService(new SweetLifeDbContext());
        }
        
        [HttpPost]
        public void Save([FromBody] Category category)
        {
            _service.Save(category);
        }

        [HttpDelete("{categoryId}")]
        public void Delete([FromRoute] long categoryId)
        {
            _service.Delete(categoryId);
        }
    }
}