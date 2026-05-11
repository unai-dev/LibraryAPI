using LibraryAPI.DTOs.Book;
using LibraryAPI.DTOs.Filter;
using LibraryAPI.Exceptions;
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
        public async Task<ActionResult<IEnumerable<BookDTO>>> Get([FromQuery] BookFilterDTO filter)
        {
            var result = await bookService.GetBooksAsync(filter);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<BookDTO>> Get([FromRoute] int id)
        {
            try
            {
                var result = await bookService.GetBooByIdAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDTO bookDTO)
        {
            try
            {
                var result = await bookService.CreateBookAsync(bookDTO);
                return CreatedAtRoute("GetById", new { id = result.Id }, result);
            }
            catch (BadRequestException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = "Error interno del servidor" });
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await bookService.DeleteBookAsync(id);

            return !result ? NotFound() : NoContent();
        }
    }
}
