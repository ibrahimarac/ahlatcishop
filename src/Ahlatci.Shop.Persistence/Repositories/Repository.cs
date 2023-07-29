using Ahlatci.Shop.Application.Repositories;
using Ahlatci.Shop.Domain.Common;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ahlatci.Shop.Persistence.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly AhlatciContext _dbContext;

        public Repository(AhlatciContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().AnyAsync(filter);
        }

        public async Task<T> GetById(object id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(object id)
        {
            await _dbContext.Set<T>().FindAsync(id);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
