using System;
using HelloApi.Models;

namespace HelloApi.Repository
{
	public interface ITodoItemRepository : IDisposable
	{
        Task<IEnumerable<TodoItem>> GetAllAsync();

        Task<TodoItem> GetByIdAsync(long id);

        Task AddAsync(TodoItem todoItem);

        Task RemoveAsync(long id);
    }
}

