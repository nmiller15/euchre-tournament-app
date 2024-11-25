using Fleck;
using Newtonsoft.Json;

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
    /// Represents the connection object that a User is currently using to interact with the server.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IWebSocketConnection Connection { get; set; }
    
    /// <summary>
    /// Represents the state of the user in the pre-game lobby of a room. All users must be ready to start the game.
    /// </summary>
    public bool IsReady { get; set; }
    
    /// <summary>
    /// Represents whether is user object is being created only to indicate which other users are given a bye round.
    /// </summary>
    public bool IsByeIndicator { get; private set; }
    
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
    
    public UserModel(string guid, string username, bool isHost, IWebSocketConnection connection)
    {
        Username = username;
        Guid = guid;
        Connection = connection;
        IsReady = false;
        IsByeIndicator = false;
        IsHost = isHost;
        RoundEntries = new List<RoundEntryModel>();
        TotalScore = 0;
        TotalLoners = 0;
    }

    [JsonConstructor]
    public UserModel(string username, string guid, bool isReady, bool isByeIndicator,
        List<RoundEntryModel> roundEntries, bool isHost, int totalScore, int totalLoners)
    {
        Username = username;
        Guid = guid;
        IsReady = isReady;
        IsByeIndicator = isByeIndicator;
        RoundEntries = roundEntries;
        IsHost = isHost;
        TotalScore = totalScore;
        TotalLoners = totalLoners;
    }

    public UserModel(string guid)
    {
        Guid = guid;
        IsByeIndicator = true;
    }

    public void SendMessageToUser(MessageModel message)
    {
        var messageJson = JsonConvert.SerializeObject(message, Formatting.Indented);
        Connection.Send(messageJson);
    }

    public void ReadyUpUser()
    {
        IsReady = true;
    }

    public void UnreadyUser()
    {
        IsReady = false;
    }

    public void DisconnectUser()
    {
        Connection.Close();
    }

    public void CreateEmptyRoundEntry(int roundNumber)
    {
        RoundEntries.Add(new RoundEntryModel(this.Guid, roundNumber, 0, 0));
    }
    
    public RoundEntryModel UpdateRoundEntry(int roundNumber, int score, int loners)
    {
        if (RoundEntries.Count < roundNumber)
        {
            RoundEntries.Add(new RoundEntryModel(this.Guid, roundNumber, score, 0));
        }

        var entryToUpdate = RoundEntries[roundNumber - 1];
        entryToUpdate.UpdateEntryScore(score);
        entryToUpdate.UpdateEntryLoners(loners);
        return entryToUpdate;

    }
    
    public void UpdateRoundEntry(int roundNumber, int score)
    {
        if (RoundEntries.Count < roundNumber)
        {
            RoundEntries.Add(new RoundEntryModel(this.Guid, roundNumber, score, 0));
        }
        else
        {
            var entryToUpdate = RoundEntries[roundNumber - 1];
            entryToUpdate.Score = score;
        }
    }
}