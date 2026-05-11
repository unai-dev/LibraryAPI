using AutoMapper;
using LibraryAPI.DTOs.Category;
using LibraryAPI.Entities;

namespace LibraryAPI.Utils.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
