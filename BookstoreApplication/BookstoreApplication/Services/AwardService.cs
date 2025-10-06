using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class AwardService
    {
        private readonly AwardsRepository _awardsRepository;

        public AwardService(BookstoreDbContext context)
        {
            _awardsRepository = new AwardsRepository(context);
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

        public async Task<Award> UpdateAsync(Award award, Award existingAward)
        {
            existingAward.Id = award.Id;
            existingAward.Name = award.Name;
            existingAward.Description = award.Description;
            existingAward.AwardStartYear = award.AwardStartYear;
            return await _awardsRepository.UpdateAsync(existingAward);
        }

        public async Task DeleteAsync(Award award)
        {
            await _awardsRepository.DeleteAsync(award);
        }
    }
}
