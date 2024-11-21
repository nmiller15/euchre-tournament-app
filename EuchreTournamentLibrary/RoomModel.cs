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

    public int CurrentRound { get; private set; } = 0;
    
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

    public void GenerateSchedule()
    {
        var numUsers = Users.Count;
        var numUsersPerTable = 4;
        var numTablesPerRound = numUsers / numUsersPerTable;
        var numUsersSittingPerRound = numUsers - (numTablesPerRound * numUsersPerTable);
        var numRounds = numUsers + numUsersSittingPerRound - 1;

        var rounds = new List<RoundModel>();

        var usersList = Users.Values.ToList();

        for (var i = 0; i < numUsersSittingPerRound; i++)
        {
            usersList.Add(new UserModel(System.Guid.NewGuid().ToString()));
        }

        for (var j = 0; j < numRounds; j++)
        {
            var roundNumber = j + 1;
            var mid = usersList.Count / 2;

            var firstHalf = usersList.GetRange(0, mid);
            var secondHalf = usersList.GetRange(mid, mid); 
            
            secondHalf.Reverse();

            List<UserModel> playersSittingOut = new List<UserModel>();
            List<TeamModel> teams = new List<TeamModel>();
            
            for (var k = 0; k < mid; k++)
            {
                if (firstHalf[k].IsByeIndicator && secondHalf[k].IsByeIndicator) continue;
                if (firstHalf[k].IsByeIndicator)
                {
                    playersSittingOut.Add(Users[secondHalf[k].Guid]);
                }
                else if (secondHalf[k].IsByeIndicator)
                {
                    playersSittingOut.Add(Users[firstHalf[k].Guid]);
                }
                else
                {
                    List<UserModel> teamUsers =
                    [
                        Users[firstHalf[k].Guid],
                        Users[secondHalf[k].Guid]
                    ];
                    TeamModel team = new TeamModel(roundNumber, teamUsers);
                    teams.Add(team);
                }
            }

            List<TableModel> tables = new List<TableModel>();

            var teamsLeft = 0;
            var teamsRight = teams.Count - 1;
            var extraTeam = teams.Count % 2;

            while (teamsRight > teamsLeft + extraTeam)
            {
                List<TeamModel> tableTeams = new List<TeamModel> { teams[teamsLeft], teams[teamsRight] };
                tables.Add(new TableModel(teamsLeft + 1, roundNumber, tableTeams));
                teamsLeft++;
                teamsRight--;
            }

            if (extraTeam == 1)
            {
                playersSittingOut.Add(teams[teamsRight].Players[0]);
                playersSittingOut.Add(teams[teamsRight].Players[1]);
            }
            
            rounds.Add(new RoundModel(roundNumber, tables, playersSittingOut));

            var lastIndex = usersList.Count - 1;
            var lastUser = usersList[lastIndex];
            usersList.Remove(lastUser);
            usersList.Insert(1, lastUser);
        }

        Schedule = rounds;
    }
}