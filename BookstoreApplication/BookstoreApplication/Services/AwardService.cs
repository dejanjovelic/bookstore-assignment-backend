using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardsRepository _awardsRepository;

        public AwardService(IAwardsRepository awardsRepository)
        {
            _awardsRepository = awardsRepository;
        }

        public async Task<List<Award>> GetAllAsync()
        {
            return await _awardsRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Award> GetByIdAsync(int id)
        {
            return await _awardsRepository.GetByIdAsync(id);
        }

        public async Task<Award> CreateAsync(Award award)
        {
            return await _awardsRepository.CreateAsync(award);
        }

        public async Task<Award> UpdateAsync(Award award)
        {

            Award existingAward = await GetByIdAsync(award.Id);
            if (existingAward == null)
            {
                throw new ArgumentException($"Award with ID {award.Id} not found.");
            }
            existingAward.Id = award.Id;
            existingAward.Name = award.Name;
            existingAward.Description = award.Description;
            existingAward.AwardStartYear = award.AwardStartYear;
            return await _awardsRepository.UpdateAsync(existingAward);
        }

        public async Task DeleteAsync(int id)
        {
            Award award = await GetByIdAsync(id);
            if (award == null)
            {
                throw new ArgumentException($"Award with ID {id} not found.");
            }
            await _awardsRepository.DeleteAsync(award);
        }
    }
}
