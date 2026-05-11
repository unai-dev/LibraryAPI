using LibraryAPI.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateCategoryAsync([FromBody] CreateCategoryDTO categoryDTO);
        Task<bool> DeleteCategoryAsync([FromRoute] int id);
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetCategoryAsync([FromRoute] int id);
    }
}