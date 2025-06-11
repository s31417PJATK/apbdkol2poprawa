using APBD_kol2.DTOs;
using APBD_kol2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_kol2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class charactersController : ControllerBase
    {
        private readonly Ikol2Service _kol2Service;

        public charactersController(Ikol2Service kol2Service)
        {
            _kol2Service = kol2Service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var res = await _kol2Service.getCharacter(id);
            if (res == null) return NotFound("Character not found");
            return Ok(res);
        }

        [HttpPost("{id}/backpacks")]
        public async Task<IActionResult> postBackpacks(int id,[FromBody] List<int> items)
        {
            var res = await _kol2Service.postBackpacks(id, items);
            if (res == "added") return Ok();
            if (res == "Too much weight") return BadRequest("Too much weight");
            if (res == "Item not found") return BadRequest("Item not found");
            if (res == "Character not found") return BadRequest("Character not found");
            if (res == "list is empty") return BadRequest("List is empty");
            return Conflict();
        }
    }
}
