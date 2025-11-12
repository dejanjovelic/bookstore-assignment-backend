using BookstoreApplication.Models;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Services.IServices;
using BookstoreApplication.Services.DTO;

namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorsRepository _authorsRepository;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorsRepository authorsRepository, ILogger<AuthorService> logger)
        {
            this._authorsRepository = authorsRepository;
            this._logger = logger;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            _logger.LogInformation($"Geting all authors.");
            return await _authorsRepository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Get author with id {id}.");
            Author  author = await _authorsRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new NotFoundException($"Author with ID {id} not found.");
            }
            _logger.LogInformation($"The author with Id: {id} exists.");
            return author;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            _logger.LogInformation($"Created Author Id:{author.Id}");
            return await _authorsRepository.CreateAsync(author);
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            _logger.LogInformation($"Updating author with ID: {id}");
            if (author.Id != id)
            {
               throw new BadRequestException($"Author ID mismatch: route ID {id} vs body ID {author.Id}");
            }

            Author existingAuthor = await _authorsRepository.GetByIdAsync(author.Id);
            if (existingAuthor == null)
            {
                throw new NotFoundException($"Author with ID {author.Id} not found.");
            }

            existingAuthor.DateOfBirth = author.DateOfBirth;
            existingAuthor.FullName = author.FullName;
            existingAuthor.Biography = author.Biography;
            existingAuthor.Id = author.Id;

            _logger.LogInformation($"Author with ID: {id} updated. ");
            return await _authorsRepository.UpdateAsync(existingAuthor);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting author with ID: {id}");
            Author author = await GetByIdAsync(id);
            if (author == null)
            {
                throw new NotFoundException($"Author with ID {id} not found.");
            }
            _logger.LogInformation($"Author with ID: {author.Id} deleted");
            await _authorsRepository.DeleteAsync(author);
        }
        public async Task<PaginatedListDto<Author>> GetAllAuthorsPaginatedAsync(int page, int pageSize) 
        {
            if (page < 1)
            {
                throw new BadRequestException("Page must be greather than 0.");
            }
            if (pageSize < 1)
            {
                throw new BadRequestException("The value must be a positive integer greater than zero.");
            }
            PaginatedListDto<Author> paginatedListAutors = await _authorsRepository.GetAllAuthorsPaginatedAsync(page, pageSize);
            return paginatedListAutors;
        }
    }
}
