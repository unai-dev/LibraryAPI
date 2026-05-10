namespace LibraryAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        // Propiedad de navegacion
        public Author? Author { get; set; }
        public int AuthorId { get; set; }
    }
}
