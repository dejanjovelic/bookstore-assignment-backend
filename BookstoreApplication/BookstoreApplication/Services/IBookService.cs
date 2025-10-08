using BookstoreApplication.DTO;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDetailsDto> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(int id);

    }
}
