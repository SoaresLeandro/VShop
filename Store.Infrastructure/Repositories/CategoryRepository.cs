using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;
using Store.Infrastructure.Context;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetWithProducts()
        {
            return await _appDbContext.Categories.Include(c => c.Products).AsNoTracking().ToListAsync();
        }
    }
}