namespace LibraryAPI.DTOs.Book
{
    public class CreateBookDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
