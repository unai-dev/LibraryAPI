using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required, MinLength(3)]
        public required string Name { get; set; }
    }
}
