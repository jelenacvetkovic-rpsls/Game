using Google.Protobuf.WellKnownTypes;
using Game.Core.Interfaces;
using Game.Core.Models;
using static ChoiceService.Choice;

namespace Game.Infrastructure.Clients
{
    public class ChoiceGrpcClient : IChoiceGrpcClient
    {
        private readonly ChoiceClient _client;

        public ChoiceGrpcClient(ChoiceClient client)
        {
            _client = client;
        }

        public async Task<ChoiceDto> GetRandomChoiceAsync()
        {
            var randomChoice = await _client.GetRandomChoiceAsync(new Empty());
            return new ChoiceDto { Id = randomChoice.Id, Name = randomChoice.Name };
        }
    }
}
