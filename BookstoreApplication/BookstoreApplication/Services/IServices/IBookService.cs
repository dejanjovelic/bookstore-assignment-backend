using BookstoreApplication.Models;
using BookstoreApplication.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        Task<PaginatedListDto<BookDetailsDto>> GetSortedAndPaginatedBooksAsync(int sortType, int page, int pageSize);
        Task<PaginatedListDto<BookDetailsDto>> GetFilteredAndSortedAndPaginatedBooksAsync(BookFilterDto filterDto, int sortType, int page, int pageSize);
    }
}
