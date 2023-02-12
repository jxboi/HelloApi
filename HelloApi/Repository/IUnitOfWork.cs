namespace HelloApi.Repository
{
	public interface IUnitOfWork
	{
		ITodoItemRepository Todo { get; }
        Task SaveAsync();
    }
}

