namespace BookstoreApplication.Models
{
    public enum BookSortType
    {
        BOOK_TITLE_ASC,
        BOOK_TITLE_DESC,
        PUBLISHED_DATE_ASC,
        PUBLISHED_DATE_DESC,
        AUTHORS_FULLNAME_ASC,
        AUTHORS_FULLNAME_DESC
    }
}

//nazivu knjige(rastuće i opadajuće)
//datumu izdavanja knjige (rastuće i opadajuće)
//imenu autora knjige (rastuće i opadajuće)
//Ako sortiranje nije izabrano (npr. prilikom prve posete ovoj stranici),
//sortirati podatke prema nazivu u rastućem redosledu.
//Izbor tipa sortiranja implementirati pomoću padajućeg menija.
