using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {

        public ReadRepository(ProjectDbContext context)
        {
            _context = context;
        }


        private readonly ProjectDbContext _context;

        public DbSet<T> Table => _context.Set<T>();

        public async Task<IQueryable<T>> GetAll(bool tracking = true)
        {
            return await Task.Run(() =>
            {
                IQueryable<T> query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                return query;
            });

        }
        public async Task<IQueryable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table.Where(predicate);
            return await Task.Run(() =>
            {
                if (includes != null)
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }
                return query;
            });
        }

        public async Task<T> GetSingleWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table.Where(predicate);
            return await Task.Run(() =>
            {
                if (includes != null)
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }
                return query.FirstOrDefaultAsync();
            });
        }

        public async Task<IQueryable<T>> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            return await Task.Run(() =>
            {
                var query = Table.Where(method);
                if (!tracking)
                    query = query.AsNoTracking();
                return query;
            });
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await Table.FindAsync(Guid.Parse(id));
        }
    }
}
