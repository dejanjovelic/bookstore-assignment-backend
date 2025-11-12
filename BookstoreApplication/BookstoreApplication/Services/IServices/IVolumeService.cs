using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services.IServices
{
    public interface IVolumeService
    {
        Task<List<VolumeDto>> GetFilteredVolumesByNameAsync(string query);

    }
}