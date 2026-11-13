using Store.Domain.Entities;

namespace Store.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetWithProducts();
    }
}