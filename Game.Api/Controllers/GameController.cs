using Game.Core.Interfaces;
using Game.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Game.Api.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("play")]
        public async Task<IActionResult> Play([FromBody] GameRequest request)
        {
            var result = await _gameService.PlayGameAsync(request);
            return Ok(result);
        }
    }
}
