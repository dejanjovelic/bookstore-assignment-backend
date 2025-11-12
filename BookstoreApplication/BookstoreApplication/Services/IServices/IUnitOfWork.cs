namespace BookstoreApplication.Services.IServices
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveAsync();
    }
}