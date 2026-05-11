namespace LibraryAPI.DTOs.Filter
{
    public class BookFilterDTO
    {
        public string? Title { get; set; }
        public string? OrderBy { get; set; } = "Title";
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Limit { get; set; }
        public string? AuthorName { get; set; }
    }
}
