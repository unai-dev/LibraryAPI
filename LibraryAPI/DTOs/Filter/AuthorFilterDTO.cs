namespace LibraryAPI.DTOs.Filter
{
    public class AuthorFilterDTO
    {
        public string? Name { get; set; }
        public string? OrderBy { get; set; } = "Name";
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? MinBooks { get; set; }
        public int? Limit { get; set; }

    }
}
