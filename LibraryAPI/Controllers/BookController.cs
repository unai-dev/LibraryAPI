using LibraryAPI.DTOs.Book;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("v1/api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> Get()
        {
            var result = await bookService.GetBooksAsync();

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<BookDTO>> Get([FromRoute] int id)
        {
            var result = await bookService.GetBooByIdAsync(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDTO bookDTO)
        {
            var result = await bookService.CreateBookAsync(bookDTO);

            return result is null ? NotFound() : CreatedAtRoute("GetById", new { id = result.Id }, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await bookService.DeleteBookAsync(id);

            return !result ? NotFound() : NoContent();
        }
    }
}
