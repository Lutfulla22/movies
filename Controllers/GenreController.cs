using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using movies.Mappers;
using movies.Models;
using movies.Services;

namespace movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewGenre genre)
        {
            var result = await _genreService.CreateAsync(genre.ToEntity());

            if (result.IsSuccess)
            {
                return Ok(result.Genre);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await _genreService.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
            => Ok(await _genreService.GetAsync(id));

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
            => Ok(await _genreService.DeleteAsync(id));
    }
}