using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Author
{
    public class CreateAuthorDTO
    {
        [Required, MinLength(5)]
        public required string Name { get; set; }

        [Required, Range(12, 90)]
        public int Age { get; set; }
    }
}
