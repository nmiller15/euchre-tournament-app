namespace EuchreTournamentLibrary;

public class RoomModel
{
    /// <summary>
    /// Represents the name that will be used to describe the room to players once joined.
    /// </summary>
    public string RoomName { get; set; }
    
    /// <summary>
    /// Represents the unique four-digit string that is used by players to join the room.
    /// </summary>
    public string RoomCode { get; private set; }
    
    /// <summary>
    /// The PlayerModel object of the player who created the room, identified as the host.
    /// </summary>
    public PlayerModel HostPlayer { get; set; }
    
    /// <summary>
    /// A list of players in descending order by score, ties are broken by number of loners.
    /// </summary>
    public List<PlayerModel> Players { get; set; }
    
    /// <summary>
    /// A list of rounds in ascending order by round number.
    /// All other schedule information is located within the RoundModel.
    /// </summary>
    public List<RoundModel> Schedule { get; set; }
    
    /// <summary>
    /// Instantiates a room class from a name for the room and a host player object.
    /// </summary>
    /// <param name="roomName">
    /// Represents the name that will be used to describe the room to players once joined.
    /// </param>
    /// <param name="hostPlayer">
    /// The PlayerModel object of the player who is creating the room, identified as the host.
    /// </param>
    public RoomModel(string roomName, PlayerModel hostPlayer)
    {
        this.RoomName = roomName;
        this.GenerateRoomCode();
        this.HostPlayer = hostPlayer;
        this.Players = new List<PlayerModel> { this.HostPlayer };
        this.Schedule = new List<RoundModel>();
    }

    /// <summary>
    /// Creates and attaches random string of alpha characters that players can use to join the room.
    /// Will not create a code if one has already been generated.
    /// </summary>
    /// <
    private void GenerateRoomCode()
    {
        // TODO: Implement a random generator
        this.RoomCode =  "AXTR";
    }
}