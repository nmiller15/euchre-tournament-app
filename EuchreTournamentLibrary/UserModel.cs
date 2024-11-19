namespace EuchreTournamentLibrary;

public class UserModel
{
    /// <summary>
    /// Represents the name that will identify the user.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Represents a unique identifier for the user.
    /// </summary>
    public string Guid { get; }
    
    /// <summary>
    /// Represents the state of the user in the pre-game lobby of a room. All users must be ready to start the game.
    /// </summary>
    public bool IsReady { get; set; }
    
    /// <summary>
    /// Represents each entry associated with a user. See RoundEntryModel for all associated properties.
    /// </summary>
    public List<RoundEntryModel> RoundEntries { get; set; }
    
    /// <summary>
    /// Represents the host status of user. True represents a host.
    /// There may only be one host per Room. 
    /// </summary>
    public bool IsHost { get; set; }
    
    /// <summary>
    /// Represents the sum of all completed round score integers.
    /// </summary>
    public int TotalScore { get; set; }
    
    /// <summary>
    /// Represents the number of loners achieved by the user in the entire tournament.
    /// </summary>
    public int TotalLoners { get; set; }
    
    /// <summary>
    /// Instantiates a new user with a provided name and host status.
    /// </summary>
    /// <param name="guid"> Represents a unique identifier generated at the creation of the user.</param>
    /// <param name="username"> Represents the name that will identify the user. </param>
    /// <param name="isHost"> Represents the sum of all completed round score integers. </param>
    public UserModel(string guid, string username, bool isHost)
    {
        this.Username = username;
        this.Guid = guid;
        this.IsReady = false;
        this.IsHost = isHost;
        this.RoundEntries = new List<RoundEntryModel>();
        this.TotalScore = 0;
        this.TotalLoners = 0;
    }
}