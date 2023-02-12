using HelloApi.Models;

namespace HelloApi.Repository
{
    public class UnitOfWork : IUnitOfWork
	{
        private readonly TodoContext _context;
        private ITodoItemRepository _todoItem;

        public UnitOfWork(TodoContext context)
		{
            _context = context;
        }

        public ITodoItemRepository Todo
        {
            get
            {
                if (_todoItem == null)
                {
                    _todoItem = new TodoItemRepository(_context);
                }
                return _todoItem;
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
        
    }
}

