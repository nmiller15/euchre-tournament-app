namespace EuchreTournamentLibrary;

public class PlayerModel
{
    /// <summary>
    /// Represents the name that will identify the player.
    /// </summary>
    public string PlayerName { get; set; }
    
    /// <summary>
    /// Represents each entry associated with a player. See RoundEntryModel for all associated properties.
    /// </summary>
    public List<RoundEntryModel> RoundEntries { get; set; }
    
    /// <summary>
    /// Represents the host status of player. True represents a host.
    /// There may only be one host per Room. 
    /// </summary>
    public bool IsHost { get; set; }
    
    /// <summary>
    /// Represents the sum of all completed round score integers.
    /// </summary>
    public int TotalScore { get; set; }
    
    /// <summary>
    /// Instantiates a new player with a provided name and host status.
    /// </summary>
    /// <param name="playerName"> Represents the name that will identify the player. </param>
    /// <param name="isHost"> Represents the sum of all completed round score integers. </param>
    public PlayerModel(string playerName, bool isHost)
    {
        this.PlayerName = playerName;
        this.IsHost = isHost;
        this.RoundEntries = new List<RoundEntryModel>();
        this.TotalScore = 0;
    }
}