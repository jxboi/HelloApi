using System.Linq.Expressions;

namespace HelloApi.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<T?> GetByIdAsync(long id);
        //Task<IQueryable<T>> GetAllAsync();
        //Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> FindAll();
        IQueryable<T?> FindByCondition(Expression<Func<T, bool>> expression);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}