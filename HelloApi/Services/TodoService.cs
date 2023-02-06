using System;
using HelloApi.Models;

namespace HelloApi.Services
{
	public class TodoService : ITodoService
	{
		public TodoService()
		{
		}

        public TodoItem Add(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TodoItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public TodoItem GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }
    }
}

