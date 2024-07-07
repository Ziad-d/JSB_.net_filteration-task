using BookManagement.API.DTOs;
using BookManagement.Domain.Models;
using BookManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);

            if (category == null)
               return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                Books = null
            };

            await _categoryService.AddCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var exisitngCategory = await _categoryService.GetCategoryAsync(id);

            if(exisitngCategory == null)
                return NotFound();

            if(id !=  exisitngCategory.CategoryId)
                return BadRequest();

            exisitngCategory.Name = categoryDTO.Name;
            exisitngCategory.Description = categoryDTO.Description;

            await _categoryService.UpdateCategoryAsync(exisitngCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}
