using System;
using HelloApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloApi.Repository
{
	public class TodoItemRepository : ITodoRepository, IDisposable
	{
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
		{
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            var todoItems = await _context.TodoItems.ToListAsync();
            return todoItems;
        }

        public async Task<TodoItem> GetByIdAsync(long id)
        {
            var todoItem = await _context.TodoItems
                .Where(t => t.Id == id)
                .SingleOrDefaultAsync();

            return todoItem ?? new TodoItem();
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        }

        public Task RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}

