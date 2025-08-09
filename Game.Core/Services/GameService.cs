using Game.Core.Interfaces;
using Game.Core.Models;

namespace Game.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IChoiceGrpcClient _choiceClient;

        private readonly Dictionary<int, int[]> _winMap = new()
        {
            [0] = new[] { 2, 3 },
            [1] = new[] { 0, 4 },
            [2] = new[] { 1, 3 },
            [3] = new[] { 1, 4 },
            [4] = new[] { 0, 2 }
        };

        public GameService(IChoiceGrpcClient choiceClient)
        {
            _choiceClient = choiceClient;
        }

        public async Task<GameResponse> PlayGameAsync(GameRequest request)
        {
            var computer = await _choiceClient.GetRandomChoiceAsync();
            var result = DetermineResult(request.Player, computer.Id);

            var response = new GameResponse
            {
                Player = request.Player,
                Computer = computer.Id,
                Result = result
            };

            return response;
        }

        private string DetermineResult(int player, int computer)
        {
            if (player == computer) return "tie";
            if (_winMap[player].Contains(computer)) return "win";
            return "lose";
        }
    }
}
