using BookstoreApplication.DTO;

namespace BookstoreApplication.Models.IRepositoies
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        IQueryable<Book> GetBaseBooks();
    }
}
