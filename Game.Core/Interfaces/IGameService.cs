using Game.Core.Models;

namespace Game.Core.Interfaces
{
    public interface IGameService
    {
        Task<GameResponse> PlayGameAsync(GameRequest request);
    }
}
