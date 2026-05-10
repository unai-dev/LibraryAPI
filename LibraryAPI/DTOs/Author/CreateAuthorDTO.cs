using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Author
{
    public class CreateAuthorDTO
    {
        [Required, MinLength(255)]
        public required string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
