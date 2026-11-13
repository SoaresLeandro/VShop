using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;
using Store.Infrastructure.Context;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductCategory
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}