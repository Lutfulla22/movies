using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using movies.Mappers;
using movies.Models;
using movies.Services;

namespace movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewActor actor)
        {
            var result = await _actorService.CreateAsync(actor.ToEntity());

            if (result.IsSuccess)
            {
                return Ok(result.Actor);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(await _actorService.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _actorService.GetAsync(id));

        // [HttpGet]
        // [Route("{fullname}")]
        // public async Task<IActionResult> GetAllAsync(string fullname)
        //     => Ok(await _actorService.GetAllAsync(fullname));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] NewActor actor)
        {
            var entity = actor.ToEntity();
            entity.Id = id;
            var result = await _actorService.UpdateAsync(entity);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
            => Ok(await _actorService.DeletAsync(id));
    }
}