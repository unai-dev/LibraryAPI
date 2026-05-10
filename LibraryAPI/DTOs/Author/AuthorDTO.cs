using LibraryAPI.DTOs.Book;

namespace LibraryAPI.DTOs.Author
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }

        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
