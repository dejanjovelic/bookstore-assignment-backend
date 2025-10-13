using BookstoreApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace BookstoreApplication.DTO
{
    public class PublisherSortTypeOptionDto
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public PublisherSortTypeOptionDto(PublisherSortType publisherSortType)
        {
            Key = (int)publisherSortType;
            Name = publisherSortType.ToString();
        }
    }
}
