using BookstoreApplication.Models;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Services.IServices;

namespace BookstoreApplication.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardsRepository _awardsRepository;
        private readonly ILogger<AwardService> _logger;

        public AwardService(IAwardsRepository awardsRepository, ILogger<AwardService> logger)
        {
            _awardsRepository = awardsRepository;
            _logger = logger;
        }

        public async Task<List<Award>> GetAllAsync()
        {
           
            return await _awardsRepository.GetAllAsync();
        }

        public async Task<Award> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get award with id {id}.");
            Award award = await _awardsRepository.GetByIdAsync(id);
            if (award == null)
            {
                
                throw new NotFoundException($"Award with ID {id} not found.");
            }
            _logger.LogInformation($"Award with ID {id} exists.");
            return award;
        }

        public async Task<Award> CreateAsync(Award award)
        {
            _logger.LogInformation($"Created award Id:{award.Id}");
            return await _awardsRepository.CreateAsync(award);
        }

        public async Task<Award> UpdateAsync(int id, Award award)
        {
            _logger.LogInformation($"Updating award with ID: {id}");
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

            _logger.LogInformation($"Award with ID: {id} updated. ");
            return await _awardsRepository.UpdateAsync(existingAward);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting award with ID: {id}");
            Award award = await GetByIdAsync(id);
            if (award == null)
            {
                throw new NotFoundException($"Award with ID {id} not found.");
            }
            _logger.LogInformation($"Award with ID: {award.Id} deleted");
            await _awardsRepository.DeleteAsync(award);
        }
    }
}
