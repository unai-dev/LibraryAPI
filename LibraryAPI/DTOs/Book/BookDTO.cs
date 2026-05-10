using LibraryAPI.DTOs.Author;

namespace LibraryAPI.DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public AuthorDTO? Author { get; set; }
    }
}
