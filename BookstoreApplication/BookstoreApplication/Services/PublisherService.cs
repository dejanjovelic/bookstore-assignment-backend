using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Infrastructure;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Infrastructure.Repositories;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly ILogger<PublisherService> _logger;

        public PublisherService(BookstoreDbContext context, ILogger<PublisherService> logger)
        {
            _publishersRepository = new PublishersRepository(context);
            _logger = logger;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            _logger.LogInformation($"Geting all publishers.");
            return await _publishersRepository.GetAllAsync();
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get publisher with id {id}.");
            Publisher publisher = await _publishersRepository.GetByIdAsync(id);
            if (publisher == null)
            {
                throw new NotFoundException($"Publisher with ID {id} not found.");
            }
            _logger.LogInformation($"The publisher with Id: {id} exists.");
            return publisher;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            _logger.LogInformation($"Created publisher Id:{publisher.Id}");
            return await _publishersRepository.CreateAsync(publisher);
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            _logger.LogInformation($"Updating publisher with ID: {id}");
            if (publisher.Id != id)
            {
                throw new BadRequestException($"Publisher ID mismatch: route ID:{id} body ID:{publisher.Id}.");
            }

            Publisher existingPublisher = await GetByIdAsync(publisher.Id);
            if (existingPublisher == null)
            {
                throw new NotFoundException($"Publisher with ID {publisher.Id} not found.");
            }
            existingPublisher.Name = publisher.Name;
            existingPublisher.Adress = publisher.Adress;
            existingPublisher.Website = publisher.Website;
            publisher.Id = publisher.Id;

            _logger.LogInformation($"Publisher with ID: {id} updated. ");
            return await _publishersRepository.UpdateAsync(existingPublisher);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting publisher with ID: {id}");
            Publisher publisher = await GetByIdAsync(id);
            if (publisher == null)
            {
                throw new NotFoundException($"Publisher with ID {id} not found.");
            }

            _logger.LogInformation($"Publisher with ID: {publisher.Id} deleted");
            await _publishersRepository.DeleteAsync(publisher);
        }

        public List<PublisherSortTypeOptionDto> GetAllSortTypes() 
        {
            _logger.LogInformation($"Geting all publishers sort option types.");
            List<PublisherSortTypeOptionDto> publisherSortTypeOptionDtos = new List<PublisherSortTypeOptionDto>();
            var enumValues = Enum.GetValues(typeof(PublisherSortType));
            foreach (PublisherSortType enumValue in enumValues)
            {
                publisherSortTypeOptionDtos.Add(new PublisherSortTypeOptionDto(enumValue));
            }
            return publisherSortTypeOptionDtos;
        }

        public async Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType) 
        {
            _logger.LogInformation($"Geting all sorted publishers.");
            return await _publishersRepository.GetSortedPublishers(sortType);
        }
    }
}
