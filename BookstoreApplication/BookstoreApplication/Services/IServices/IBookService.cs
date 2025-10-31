using BookstoreApplication.DTO;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services.IServices
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDetailsDto> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(int id, Book book);
        Task DeleteAsync(int id);
        List<BookSortTypeDto> GetAllSortTypes();
        Task<IEnumerable<BookDetailsDto>> GetSortedBooksAsync(int sortType);
        Task<IEnumerable<BookDetailsDto>> GetFilteredAnsSortedBooksAsync(BookFilterDto filterDto, int sortType );
    }
}
