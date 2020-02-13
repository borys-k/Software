using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Software.Application
{
    public interface IMainDbContext
    {
        T Get<T>(int id) where T : class;
        IQueryable<T> GetAllQuery<T>() where T : class;
        void AddEntity<T>(T entity) where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void RemoveEntity<T>(T entity) where T : class;
    }
}
