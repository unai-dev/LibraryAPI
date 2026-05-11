using LibraryAPI.DTOs.Author;
using LibraryAPI.DTOs.Filter;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("v1/api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> Get([FromQuery] AuthorFilterDTO filter)
        {
            var authors = await authorService.GetAuthorsAsync(filter);

            return authors is null ? NotFound() : Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> Get([FromRoute] int id)
        {
            try
            {
                var author = await authorService.GetAuthorByIdAsync(id);

                return Ok(author);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = "Error interno del servidor" });
            }

        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Post([FromBody] CreateAuthorDTO authorDTO)
        {
            try
            {
                var result = await authorService.CreateAuthorAsync(authorDTO);

                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = "Error interno del servidor" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await authorService.DeleteAuthorAsync(id);

            return result ? NoContent() : NotFound();
        }


    }
}
