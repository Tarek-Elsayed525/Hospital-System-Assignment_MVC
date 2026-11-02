using Hospital_System___Assignment.Date;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Hospital_System___Assignment.Reposatries
{


    public class Reposatry<T> : IReposatry<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<T> _dbset;

        public Reposatry(ApplicationDBContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter = null,
           Expression<Func<T, object>>[] Includes = null,
           bool IsTracked = true
           )
        {

            var entity = _dbset.AsQueryable();
            if (filter != null)
            {
                entity = entity.Where(filter);
            }

            if (Includes != null)
            {

                foreach (var include in Includes)
                {
                    entity = entity.Include(include);
                }

            }

            if (!IsTracked)
            {
                entity = entity.AsNoTracking();
            }
            return entity;
        }


        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _dbset.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetallAsync(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] Includes = null,
            bool IsTracked = true
            )
        {
            var entity = Query(filter, Includes, IsTracked);
            return await entity.ToListAsync();
        }


        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>>[] Includes = null,
            bool IsTracked = true
            )
        {
            var entity = Query(filter, Includes, IsTracked);
            return await entity.FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);

        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbset.RemoveRange(entity);
        }



    }
}
