using System;
using HelloApi.Models;

namespace HelloApi.Repository
{
	public interface ITodoRepository : IDisposable
	{
        Task<IEnumerable<TodoItem>> GetAllAsync();

        Task<TodoItem> GetByIdAsync(long id);

        Task AddAsync(TodoItem todoItem);

        Task RemoveAsync(long id);
    }
}

