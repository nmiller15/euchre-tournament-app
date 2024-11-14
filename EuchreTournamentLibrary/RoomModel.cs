namespace EuchreTournamentLibrary;

public class RoomModel
{
    public string RoomName { get; set; }
    public string RoomCode { get; private set; }
    public PlayerModel HostPlayer { get; set; }
    public List<PlayerModel> Leaderboard { get; set; }
    public List<RoundModel> Schedule { get; set; }

    public RoomModel(string roomName, PlayerModel hostPlayer)
    {
        this.RoomName = roomName;
        this.GenerateRoomCode();
        this.HostPlayer = hostPlayer;
        this.Leaderboard = new List<PlayerModel> { this.HostPlayer };
        this.Schedule = new List<RoundModel>();
    }

    private void GenerateRoomCode()
    {
        // TODO: Implement a random generator
        this.RoomCode =  "AXTR";
    }
}