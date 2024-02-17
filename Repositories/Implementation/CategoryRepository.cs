using CodePlus.API.Data;
using CodePlus.API.Models.Domain;
using CodePlus.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePlus.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<Category> CreateAsync(Category category)
        {
            if (category != null)
            {
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                category = null;
            }
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            _dbContext.Categories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategotory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if(existingCategotory !=  null)
            { 
                _dbContext.Entry(existingCategotory).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }
    }
}
