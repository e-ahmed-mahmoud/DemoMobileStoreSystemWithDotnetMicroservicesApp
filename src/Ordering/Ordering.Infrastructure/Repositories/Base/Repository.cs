using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities.Base;
using Ordering.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly OrderingDbContext _context;

        public Repository(OrderingDbContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includedString = null, bool stopTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (stopTracking) 
                query = query.AsNoTracking();

            if (includedString != null)
                query = includedString.Aggregate(query, (current, include) => current.Include(includedString));
            
            if (predicate != null)
                query = query.Where(predicate);
                       
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<Expression<Func<T, object>>> includes = null, bool stopTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (stopTracking)
                query = query.AsNoTracking();

            if (includes!= null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderby != null)
                return await orderby(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetAsyncById(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
