namespace Game.Core.Models
{
    public class GameResponse
    {
        public string Result { get; set; } = default!;
        public int Player { get; set; }
        public int Computer { get; set; }
    }
}
