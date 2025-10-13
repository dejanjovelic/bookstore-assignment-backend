using BookstoreApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTO
{
    public class PaginatedListDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalRowCount { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public PaginatedListDto(List<T> items, int totalRowsCount, int pageIndex, int pageSize)
        {
            Items = items;
            TotalRowCount = totalRowsCount;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalRowsCount / (double)pageSize);
            HasPreviousPage = PageIndex > 0;
            HasNextPage = pageIndex < TotalPages - 1;
        }
    }
}
