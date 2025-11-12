namespace BookstoreApplication.Services.Exceptions
{
    public class UnauthorizedApiAccessException : ApiComunicationException
    {
        public UnauthorizedApiAccessException() : base("An invalid API key was provided for accessing external API.")
        {
        }
    }
}
