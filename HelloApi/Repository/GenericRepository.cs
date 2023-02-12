using System;
using System.Linq.Expressions;
using HelloApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TodoContext _context;
        protected readonly DbSet<T> _table;

        public GenericRepository(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _table = context.Set<T>();
        }

        //public async Task<T?> GetByIdAsync(long id) => await _table.FindAsync(id);
        //public async Task<IEnumerable<T>> GetAllAsync() => await _table.AsNoTracking().ToListAsync();
        //public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression) => await _table.Where(expression).AsNoTracking().ToListAsync();

        public void Add(T entity) => _table.Add(entity);
        //public void AddRange(IEnumerable<T> entities) => _table.AddRange(entities);
        public void Update(T entity) => _table.Update(entity);
        public void Remove(T entity) => _table.Remove(entity);
        //public void RemoveRange(IEnumerable<T> entities) => _table.RemoveRange(entities);

        public IQueryable<T> FindAll()
        {
            return _table.AsNoTracking();
        }

        public IQueryable<T?> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).AsNoTracking();
        }
    }
}

