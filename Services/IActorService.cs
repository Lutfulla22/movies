using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movies.Entities;

namespace movies.Services
{
    public interface IActorService
    {
        Task<bool> ExistsAsync(Guid id);
        Task<Actor> GetAsync(Guid id);
        Task<List<Actor>> GetAllAsync();
        Task<List<Actor>> GetAllAsync(string fullname);
        Task<(bool IsSuccess, Exception Exception, Actor Actor)> CreateAsync(Actor actor);
        Task<(bool IsSuccess, Exception Exception)> UpdateAsync(Actor actor);
        Task<(bool IsSuccess, Exception Exception)> DeletAsync(Guid id);
    }
}