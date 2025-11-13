using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Models.IRepositoies
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<PaginatedListDto<Book>> GetSortedAndPaginatedBooksAsync(int sortType, int page, int pageSize);
        Task<PaginatedListDto<Book>> GetFilteredAndSortedAndPaginatedBooksAsync(BookFilterDto filterDto, int sortType, int page, int PageSize);
       
    }
}
