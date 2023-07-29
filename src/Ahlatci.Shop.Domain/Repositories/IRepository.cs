﻿using Ahlatci.Shop.Domain.Common;
using System.Linq.Expressions;

namespace Ahlatci.Shop.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetByFilterAsync(Expression<Func<T,bool>> filter);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<T> GetById(object id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(object id);
    }
}