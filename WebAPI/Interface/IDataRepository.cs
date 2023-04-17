using MyAppAPI.Models;
using System.Linq.Expressions;

namespace MyAppAPI.Interface
{
    public interface IDataRepository  
    {
        Task<List<T>> Get<T>()  where T : class;
        Task<T> GetById<T>(object id) where T : class;
        Task<bool> Update<T>(T entity) where T : class;
        Task<bool> Create<T>(T entity) where T : class;
        Task<List<TT>> FindAllAsync<TT>(Expression<Func<TT, bool>> filter = null, Func<IQueryable<TT>, IOrderedQueryable<TT>> orderBy = null, string includeProperties = "") where TT : class;
    }
}
