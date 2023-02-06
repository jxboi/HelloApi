using System;
using HelloApi.Models;

namespace HelloApi.Services
{
	public interface ITodoService
	{
        IEnumerable<TodoItem> GetAll();

        TodoItem GetById(long id);

        TodoItem Add(TodoItem todoItem);
        
        void Remove(long id);
    }
}

