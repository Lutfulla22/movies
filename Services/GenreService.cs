using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using movies.Data;
using movies.Entities;

namespace movies.Services
{
    public class GenreService : IGenreService
    {
        private readonly MoviesContext _context;
        private readonly ILogger<GenreService> _logger;

        public GenreService(MoviesContext context, ILogger<GenreService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception, Genre Genre)> CreateAsync(Genre genre)
        {
            if (await ExistsAsync(genre.Name))
            {
                return (false, new Exception(nameof(genre.Name)), null);
            }
            try
            {
                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Genre created in DB. ID: {genre.Id}");
                return (true, null, genre);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Creating Genre in DB failed.");
                return (false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
        {
            var genre = await GetAsync(id);
            if (genre == default(Genre))
            {
                return (false, new Exception("Not found."));
            }
            try
            {
                _context.Remove(genre);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Genre deleted in DB. ID: {genre.Id}");
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Deleting Genre in DB failed.");
                return (false, new Exception());
            }
        }

        public Task<bool> ExistsAsync(Guid id)
            => _context.Genres.AnyAsync(g => g.Id == id);

        public Task<bool> ExistsAsync(string name)
            => _context.Genres.AnyAsync(g => g.Name == name);

        public Task<List<Genre>> GetAllAsync()
            => _context.Genres.ToListAsync();

        public Task<Genre> GetAsync(Guid id)
            => _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }
}