using Ahlatci.Shop.Application.Repositories;
using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.UWork
{
    public interface IUnitWork
    {
        public IRepository<T> GetRepository<T>() where T : BaseEntity;
        public Task<bool> CommitAsync();
    }
}
