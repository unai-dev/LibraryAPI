using LibraryAPI.DTOs.Category;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("v1/api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var result = await categoryService.GetCategoriesAsync();

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var result = await categoryService.GetCategoryAsync(id);
                return CreatedAtRoute("GetById", new { id = result.Id }, result);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] CreateCategoryDTO categoryDTO)
        {
            var result = await categoryService.CreateCategoryAsync(categoryDTO);

            return result is not null ? Ok(result) : BadRequest();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);

            return result ? NoContent() : BadRequest();
        }

    }
}
