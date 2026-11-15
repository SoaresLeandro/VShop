using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs;
using Store.Application.Services.Interfaces;

namespace Store.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categoriesDto = await _categoryService.GetCategories();

            if(!categoriesDto.Any())
                return NotFound("Categories not found");

            return Ok(categoriesDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetWithProducts()
        {
            var categoriesDto = await _categoryService.GetCategoriesWithProducts();

            if (!categoriesDto.Any())
                return NotFound("Categories not found");

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);

            if (categoryDto is null)
                return NotFound("Category not found");

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest("Invalid Data");

            var categoryDto = await _categoryService.AddCategory(categoryDTO);

            if (categoryDto is null)
                return NotFound("Category not created");

            return new CreatedAtRouteResult("GetCategory", new { categoryDto });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDTO)
        {
            if (!categoryDTO.Id.Equals(id))
                return BadRequest();

            if (categoryDTO is null)
                return BadRequest("Invalid Data");

            var categoryDto = await _categoryService.UpdateCategory(categoryDTO);

            if (categoryDto is null)
                return NotFound("Category not updated");

            return new CreatedAtRouteResult("GetCategory", new { categoryDto });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            await _categoryService.RemoveCategory(id);

            return Ok();
        }
    }
}