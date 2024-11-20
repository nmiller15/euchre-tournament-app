using Newtonsoft.Json;

namespace EuchreTournamentLibrary;

public class RoomModel
{
    /// <summary>
    /// Represents the unique four-digit string that is used by players to join the room.
    /// </summary>
    public string RoomCode { get; private set; }
    
    /// <summary>
    /// The UserModel object of the player who created the room, identified as the host.
    /// </summary>
    public UserModel HostUser { get; set; }
    
    /// <summary>
    /// Marked true when a ReadyUp message is submitted by a user and all players are ready to start.
    /// This property is checked when the host attempts to start the game.
    /// </summary>
    public bool IsReady { get; set; }
    
    /// <summary>
    /// A Dictionary of players, defined by their Guid.
    /// </summary>
    public Dictionary<string, UserModel> Users { get; set; }
    
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
    /// <param name="hostUser">
    /// The UserModel object of the player who is creating the room, identified as the host.
    /// </param>
    public RoomModel(UserModel hostUser)
    {
        RoomCode = GenerateRoomCode();
        HostUser = hostUser;
        IsReady = false;
        Users = new Dictionary<string, UserModel>();
        AddUser(hostUser.Guid, hostUser);
        Schedule = new List<RoundModel>();
    }

    /// <summary>
    /// Creates and attaches random string of alpha characters that players can use to join the room.
    /// Will not create a code if one has already been generated.
    /// </summary>
    private string GenerateRoomCode()
    {
        // Random random = new Random();
        // string code = "";
        // for (int i = 0; i < 4; i++)
        // {
        //     int randomNumber = random.Next(0, 26);
        //     char randomAlpha = (char)('A' + randomNumber);
        //
        //     code = $"{code}{randomAlpha}";
        // }
        // return code;
        // For Dev:
        return "ABCD";
    }
    
    public void AddUser(string guid, UserModel user)
    {
        Users.Add(guid, user);
    }

    public void BroadcastToRoom(MessageModel message)
    {
        var jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
        foreach (var guid in Users.Keys)
        {
            Users[guid].Connection.Send(jsonMessage);
        }
    }

    public bool CheckUsersReadyState()
    {
        if (Users.Count < 4) return false;
        foreach (var uuid in Users.Keys)
        {
            if (!Users[uuid].IsReady) return false;
        }

        return true;
    }
}