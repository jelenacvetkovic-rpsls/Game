public class GamePlayedEvent
{
    public string? UserId { get; set; }
    public int PlayerChoice { get; set; }
    public int ComputerChoice { get; set; }
    public string Result { get; set; }
    public DateTime PlayedAtUtc { get; set; } = DateTime.UtcNow;
}