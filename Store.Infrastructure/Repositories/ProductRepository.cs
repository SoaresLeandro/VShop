using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;
using Store.Infrastructure.Context;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _appDbContext.Products.Include(p => p.Category).OrderBy(p => p.Name).ToListAsync();
        }

        public override async Task<Product> GetById(int id)
        {
            return await _appDbContext.Products.Include(p => p.Category)
                                               .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}