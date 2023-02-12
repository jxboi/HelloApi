using HelloApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repository
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
	{
        public TodoItemRepository(TodoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<TodoItem?> GetTodoItemByIdAsync(long ownerId)
        {
            return await FindByCondition(owner => owner.Id.Equals(ownerId)).FirstOrDefaultAsync();
        }

        public void AddTodoItem(TodoItem item)
        {
            Add(item);
        }

        public void RemoveTodoItem(TodoItem item)
        {
            Remove(item);
        }

        public void UpdateTodoItem(TodoItem item)
        {
            Update(item);
        }
    }
}

