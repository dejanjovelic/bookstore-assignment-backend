using BookstoreApplication.DTO;

namespace BookstoreApplication.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public string AuthorFullName { get; set; }
        public string PublisherName {  get; set; }
        public int YearsSincePublication { get; set; }

    }
}




//BookDto koja sadrži osnovne informacije knjige:
//id, naslov, ISBN, puno ime autora, naziv izdavača,
//pre koliko godina je nastala knjiga
//(razlika između sadašnje godine i godine
//kada je izdata knjiga).
//GetAll metoda serverske aplikacije
//vraća listu ovih objekata.
