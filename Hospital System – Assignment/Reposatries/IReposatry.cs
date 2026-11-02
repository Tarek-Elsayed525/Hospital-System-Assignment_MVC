using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Hospital_System___Assignment.Reposatries
{

    public interface IReposatry<T> where T : class
    {
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] Includes = null,
            bool IsTracked = true
            );

        Task<EntityEntry<T>> AddAsync(T entity);

        Task<IEnumerable<T>> GetallAsync(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] Includes = null,
            bool IsTracked = true
            );

        Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] Includes = null,
            bool IsTracked = true
            );




        void Update(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);



    }
}
