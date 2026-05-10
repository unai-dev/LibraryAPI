using LibraryAPI.DTOs.Author;
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
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> Get()
        {
            var authors = await authorService.GetAuthorsAsync();

            return authors is null ? NotFound() : Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDTO>> Get([FromRoute] int id)
        {
            var author = await authorService.GetAuthorByIdAsync(id);

            return author is null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Post([FromBody] CreateAuthorDTO authorDTO)
        {
            var result = await authorService.CreateAuthorAsync(authorDTO);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await authorService.DeleteAuthorAsync(id);

            return result ? NoContent() : NotFound();
        }


    }
}
