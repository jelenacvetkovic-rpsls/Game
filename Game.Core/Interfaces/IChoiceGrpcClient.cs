using Game.Core.Models;

namespace Game.Core.Interfaces
{
    public interface IChoiceGrpcClient
    {
        Task<ChoiceDto> GetRandomChoiceAsync();
    }
}