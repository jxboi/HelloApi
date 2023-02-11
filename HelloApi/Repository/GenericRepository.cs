using System;
using System.Linq.Expressions;
using HelloApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TodoContext _context;
        internal readonly DbSet<T> _table;

        public GenericRepository(TodoContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T? GetById(int id)
        {
            return _table.Find(id);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Remove(T entity)
        {
            _table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
        }
    }
}

