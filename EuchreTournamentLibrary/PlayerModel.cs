namespace EuchreTournamentLibrary;

public class PlayerModel
{
    
    public string PlayerName { get; set; }
    public Dictionary<RoundModel, int > RoundScore { get; set; }
    public bool IsHost { get; set; }
    public int TotalScore { get; set; }
    
    public PlayerModel(string playerName, bool isHost)
    {
        this.PlayerName = playerName;
        this.IsHost = isHost;
        this.RoundScore = new Dictionary<RoundModel, int>();
        this.TotalScore = 0;
    }
}