using MyAppAPI.Context;
using MyAppAPI.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyAppAPI.Services
{
    public class DataRepository : IDataRepository 
    {
        private readonly DataContext _context;
       
        public DataRepository(DataContext context)
        {
            _context = context;
           
        }
        public async Task<List<T>> Get<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();

        }
        public async Task<T> GetById<T>(object id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<TT>> FindAllAsync<TT>(Expression<Func<TT, bool>> filter = null, Func<IQueryable<TT>, IOrderedQueryable<TT>> orderBy = null, string includeProperties = "") where TT : class
        {
              DbSet<TT> _dbSet;
            _dbSet = _context.Set<TT>();
            var  query = (IQueryable<TT>)_dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] {}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<bool> Update<T>(T entity) where T : class
        {
            if (entity != null)
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> Create<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
