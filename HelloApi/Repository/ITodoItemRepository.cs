using System;
using HelloApi.Models;

namespace HelloApi.Repository
{
	public interface ITodoItemRepository : IGenericRepository<TodoItem>, IDisposable
	{

    }
}

