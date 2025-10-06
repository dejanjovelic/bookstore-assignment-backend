using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class AuthorService
    {

        private readonly AuthorsRepository _authorsRepository;

        public AuthorService(BookstoreDbContext context)
        {
            _authorsRepository = new AuthorsRepository(context);
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

        public async Task<Author> UpdateAsync(Author author, Author existingAuthor)
        {
            existingAuthor.DateOfBirth = author.DateOfBirth;
            existingAuthor.FullName = author.FullName;
            existingAuthor.Biography = author.Biography;
            existingAuthor.Id = author.Id;

            return await _authorsRepository.UpdateAsync(existingAuthor);
        }

        public async Task DeleteAsync(Author author)
        {
            await _authorsRepository.DeleteAsync(author);
        }
    }
}
