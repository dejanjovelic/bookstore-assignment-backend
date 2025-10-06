using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class PublisherService
    {
        private readonly PublishersRepository _publishersRepository;

        public PublisherService(BookstoreDbContext context)
        {
            _publishersRepository = new PublishersRepository(context);
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _publishersRepository.GetAllAsync();
        }

        public async Task<Publisher> GetByIdAsync(int id)
        {
            return await _publishersRepository.GetByIdAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            return await _publishersRepository.CreateAsync(publisher);

        }

        public async Task<Publisher> UpdateAsync(Publisher existingPublisher, Publisher publisher)
        {
            existingPublisher.Name = publisher.Name;
            existingPublisher.Adress = publisher.Adress;
            existingPublisher.Website = publisher.Website;
            publisher.Id = publisher.Id;

            return await _publishersRepository.UpdateAsync(existingPublisher);
        }

        public async Task DeleteAsync(Publisher publisher)
        {
            await _publishersRepository.DeleteAsync(publisher);
        }

    }
}
