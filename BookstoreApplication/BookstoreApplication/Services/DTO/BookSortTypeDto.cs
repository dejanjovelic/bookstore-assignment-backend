using BookstoreApplication.Models;

namespace BookstoreApplication.Services.DTO
{
    public class BookSortTypeDto
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public BookSortTypeDto(BookSortType bookSortTypeDto)
        {
            Key = (int)bookSortTypeDto;
            Name = bookSortTypeDto.ToString();
        }
    }
}
