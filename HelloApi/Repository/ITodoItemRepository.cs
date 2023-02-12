using System;
using HelloApi.Models;

namespace HelloApi.Repository
{
	public interface ITodoItemRepository : IGenericRepository<TodoItem>
	{
        Task<TodoItem?> GetTodoItemByIdAsync(long id);
        Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync();
        
        void AddTodoItem(TodoItem item);
        void UpdateTodoItem(TodoItem item);
        void RemoveTodoItem(TodoItem item);
    }
}

