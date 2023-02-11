using HelloApi.Models;

namespace HelloApi.Repository
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
	{
        public TodoItemRepository(TodoContext context) : base(context)
        {
        }
    }
}

