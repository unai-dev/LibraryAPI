namespace LibraryAPI.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }

        // Propiedad de navegacion
        public List<Book> Books { get; set; } = new List<Book>();

    }
}
