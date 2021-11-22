using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using movies.Entities;
using movies.Mappers;
using movies.Models;
using movies.Services;

namespace movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;

        public MovieController(IMovieService movieService, IActorService actorService, IGenreService genreService)
        {
            _movieService = movieService;
            _actorService = actorService;
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewMovie movie)
        {
            if (movie.ActorsId.Count() < 1 || movie.GenresId.Count() < 1)
            {
                return BadRequest("Actors and Genres are required.");
            }
            if (!movie.ActorsId.All(id => _actorService.ExistsAsync(id).Result))
            {
                return BadRequest("Actor doesnt exists.");
            }
            if (!movie.GenresId.All(id => _genreService.ExistsAsync(id).Result))
            {
                return BadRequest("Genre doesnt exists.");
            }
            var actors = movie.ActorsId.Select(id => _actorService.GetAsync(id).Result);
            var genres = movie.GenresId.Select(id => _genreService.GetAsync(id).Result);
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(await _movieService.CreateAsync(movie.ToEntity(actors, genres)), options);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(await _movieService.GetAllAsync(), options);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
            => Ok(await _movieService.DeleteAsync(id));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(await _movieService.GetAsync(id), options);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Movie movie)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(await _movieService.UpdateAsync(movie), options);
            return Ok(result);
        }
    }
}