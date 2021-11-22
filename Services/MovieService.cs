using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using movies.Data;
using movies.Entities;

namespace movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly MoviesContext _context;
        private readonly ILogger<MovieService> _logger;

        public MovieService(MoviesContext context, ILogger<MovieService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception, Movie Movie)> CreateAsync(Movie movie)
        {
            try
            {
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Movie created in DB. ID: {movie.Id}");
                return (true, null, movie);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Creating Movie in DB failed.");
                return (false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
        {
            var movie = await GetAsync(id);
            if (movie == default(Movie))
            {
                return (false, new Exception("Not found."));
            }
            try
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Movie deleted in DB. ID: {movie.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Deleting Movie in DB failed.");
                return (false, e);
            }
        }

        public Task<bool> ExistsAsync(Guid id)
            => _context.Movies.AnyAsync(m => m.Id == id);

        public Task<List<Movie>> GetAllAsync()
            => _context.Movies.Include(m => m.Actors).Include(m => m.Genres).ToListAsync();

        public Task<List<Movie>> GetAllAsync(string title)
            => _context.Movies.AsNoTracking().Where(m => m.Title == title).ToListAsync();

        public Task<Movie> GetAsync(Guid id)
            => _context.Movies.Include(m => m.Actors).Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Movie movie)
        {
            if (!await ExistsAsync(movie.Id))
            {
                return (false, new Exception("Not found"));
            }
            try
            {
                _context.Entry(movie).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Movie updated in DB. ID: {movie.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Updating Movie in DB failed.");
                return (false, e);
            }
        }
    }
}