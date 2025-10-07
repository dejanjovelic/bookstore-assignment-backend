using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorsRepository _authorsRepository;

        public AuthorService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorsRepository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _authorsRepository.GetByIdAsync(id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            return await _authorsRepository.CreateAsync(author);
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            Author existingAuthor = await GetByIdAsync(author.Id);
            if (existingAuthor == null)
            {
                throw new ArgumentException($"Author with ID {author.Id} not found.");
            }

            existingAuthor.DateOfBirth = author.DateOfBirth;
            existingAuthor.FullName = author.FullName;
            existingAuthor.Biography = author.Biography;
            existingAuthor.Id = author.Id;

            return await _authorsRepository.UpdateAsync(existingAuthor);
        }

        public async Task DeleteAsync(int id)
        {
            Author author = await GetByIdAsync(id);
            if (author == null)
            {
                throw new ArgumentException($"Author with ID {id} not found.");
            }
            await _authorsRepository.DeleteAsync(author);
        }
    }
}
