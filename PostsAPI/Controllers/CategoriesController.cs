using Microsoft.AspNetCore.Mvc;
using PostsAPI.Models;
using PostsAPI.Repositories;

namespace PostsAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            var createdCategory = await _categoryRepository.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategories), new { id = createdCategory.Id }, createdCategory);
        }
    }
}
