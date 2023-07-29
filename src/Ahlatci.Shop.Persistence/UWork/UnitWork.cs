using Ahlatci.Shop.Application.Repositories;
using Ahlatci.Shop.Domain.Common;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Ahlatci.Shop.Persistence.UWork
{
    public class UnitWork : IUnitWork
    {
        private Dictionary<Type, object> _repositories;
        private readonly IServiceProvider _serviceProvider;
        private readonly AhlatciContext _context;

        public UnitWork(IServiceProvider serviceProvider, AhlatciContext context)
        {
            _repositories = new Dictionary<Type, object>();
            _serviceProvider = serviceProvider;
            _context = context;
        }

        /// <summary>
        /// Bu UnitOfWork katmanında kayıtlı olan tüm repolar için tek seferde
        /// db kayıt işlemini çalıştırır. Hata olursa buradan exception fırlatılır.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }                
            return true;
        }

        /// <summary>
        /// Yazılımcı hethangi bir repo üzerinde insert, update, delete veya select yapacaksa
        /// bu metod yardımıyla DI içerisinde kayıtlı ilgili entity için kullanılabilecek repoya erişir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            //Daha önce bu repoyu talep eden bir kullanıcı olmuşsa aynı repoyu tekrar üretmez.
            //Burada sakladığı koleksiyon içerisinden gönderir. Bu da performansı artırır.
            if (_repositories.ContainsKey(typeof(IRepository<T>)))
            {
                return (IRepository<T>)_repositories[typeof(IRepository<T>)];
            }

            //Eğer bu repo ilgili UnitWork için hiç kullanılmamışsa tanımlı değildir.
            //Burada DI içerisinden bu repo alınır ve bundan sonraki kullanımlarda ihtiyaç olabilir
            //düşüncesi ile sınıf içerisindeki Dictionary'de saklanır.
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<T>>();
                _repositories.Add(typeof(IRepository<T>), repository);
                return repository;
            }
        }
    }
}
