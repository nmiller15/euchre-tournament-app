namespace EuchreTournamentLibrary;

public class PlayerModel
{
    
    public string PlayerName { get; set; }
    public Dictionary<Round, int > RoundScore { get; set; }
    public bool IsHost { get; set; }
    
    public PlayerModel(string playerName, bool isHost)
    {
        this.PlayerName = playerName;
        this.IsHost = isHost;
        this.RoundScore = new Dictionary<Round, int>();
    }
}