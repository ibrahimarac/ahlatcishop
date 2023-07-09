using System.Linq.Expressions;

namespace Ahlatci.Shop.Persistence.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        //CRUD C=>Create, R=>Read, U=>Update, D=>Delete

        #region Read

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes);

        #endregion


        #region Create, Update, Delete



        #endregion
    }
}
