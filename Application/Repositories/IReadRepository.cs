using Application.PaginationParameters;
using Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll(bool tracking = true);
        Task<PaginationResult<T>> PaginateAsync<D>(Expression<Func<T, bool>> predicate, D pagination, params Expression<Func<T, object>>[] includes) where D : Pagination;


        Task<IQueryable<T>> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<IQueryable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetSingleWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
