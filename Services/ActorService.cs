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
    public class ActorService : IActorService
    {
        private readonly MoviesContext _context;
        private readonly ILogger<ActorService> _logger;

        public ActorService(MoviesContext context, ILogger<ActorService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception, Actor Actor)> CreateAsync(Actor actor)
        {
            try
            {
                await _context.Actors.AddAsync(actor);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Actor created in DB. ID: {actor.Id}");
                return (true, null, actor);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Creating Actor in DB failed.");
                return (false, e, null);
            }
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeletAsync(Guid id)
        {
            var actor = await GetAsync(id);
            if (actor == default(Actor))
            {
                return (false, new Exception("Not found."));
            }
            try
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Actor deleted in DB. ID: {actor.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Deleting Actor in DB failed.");
                return (false, e);
            }
        }

        public Task<bool> ExistsAsync(Guid id)
            => _context.Actors.AnyAsync(a => a.Id == id);

        public Task<List<Actor>> GetAllAsync()
            => _context.Actors.ToListAsync();

        public Task<List<Actor>> GetAllAsync(string fullname)
            => _context.Actors.AsNoTracking().Where(a => a.Fullname == fullname).ToListAsync();

        public Task<Actor> GetAsync(Guid id)
            => _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Actor actor)
        {
            if (!await ExistsAsync(actor.Id))
            {
                return (false, new Exception("Not found."));
            }
            try
            {
                _context.Actors.Update(actor);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Actor updated in DB. ID: {actor.Id}");
                return (true, null);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Updating Actor in DB failed.");
                return (false, e);
            }
        }
    }
}