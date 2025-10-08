using BookstoreApplication.Models;
using BookstoreApplication.Exceptions;
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

        public async Task<Award> GetByIdAsync(int id)
        {
            Award award = await _awardsRepository.GetByIdAsync(id);
            if (award == null)
            {
                throw new NotFoundException($"Award with ID {id} not found.");
            }
            return award;
        }

        public async Task<Award> CreateAsync(Award award)
        {
            return await _awardsRepository.CreateAsync(award);
        }

        public async Task<Award> UpdateAsync(int id, Award award)
        {
            if (award.Id != id)
            {
                throw new BadRequestException($"Award ID mismatch: route ID {id} vs body ID {award.Id}");
            }

            Award existingAward = await GetByIdAsync(award.Id);
            if (existingAward == null)
            {
                throw new NotFoundException($"Award with ID {award.Id} not found.");
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
                throw new NotFoundException($"Award with ID {id} not found.");
            }
            await _awardsRepository.DeleteAsync(award);
        }
    }
}
