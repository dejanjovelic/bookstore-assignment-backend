namespace BookstoreApplication.Exceptions
{
    public class ApiComunicationException : Exception
    {
        public ApiComunicationException(string message): base(message) 
        { 
        }
    }
}
