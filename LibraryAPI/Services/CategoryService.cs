using AutoMapper;
using LibraryAPI.Config;
using LibraryAPI.DTOs.Category;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoryService(ApplicationDbContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await context.Categories.ToListAsync();

            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryAsync([FromRoute] int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
            {
                throw new NotFoundException("Categoria no encontrada");
            }

            return mapper.Map<CategoryDTO>(category);

        }

        public async Task<CategoryDTO> CreateCategoryAsync([FromBody] CreateCategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);

            context.Add(category);
            await context.SaveChangesAsync();

            return mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync([FromRoute] int id)
        {
            var result = await context.Categories.Where(x => x.Id == id).ExecuteDeleteAsync();

            return result > 0;
        }
    }
}
