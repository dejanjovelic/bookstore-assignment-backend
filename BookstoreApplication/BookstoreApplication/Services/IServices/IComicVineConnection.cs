namespace BookstoreApplication.Services.IServices
{
    public interface IComicVineConnection
    {
        Task<string> Get(string url);
    }
}