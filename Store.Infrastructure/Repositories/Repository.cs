using Microsoft.EntityFrameworkCore;
using Store.Domain.Interfaces.Repositories;
using Store.Infrastructure.Context;
using System.Linq.Expressions;

namespace Store.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _appDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            T entity = await _appDbContext.Set<T>().Where(expression).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<T> Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);

            _appDbContext.SaveChanges();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);

            _appDbContext.SaveChanges();

            return entity;
        }

        public async Task Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);

            _appDbContext.SaveChanges();
        }
    }
}
