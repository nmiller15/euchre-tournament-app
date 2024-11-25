using Newtonsoft.Json;

namespace EuchreTournamentLibrary;

public class ResultsModel
{
    public List<UserModel> Leaderboard { get; private set; }

    public UserModel Award1 { get; private set; }

    // public UserModel Award2 { get; private set; }


    public ResultsModel(RoomModel room)
    {
        Leaderboard = GenerateLeaderboard(room.Users);
        Award1 = GenerateAward1(room.Users);
    }

    private List<UserModel> GenerateLeaderboard(Dictionary<string, UserModel> users)
    {
        return new List<UserModel>();
    }

    private UserModel GenerateAward1(Dictionary<string, UserModel> users)
    {
        return new UserModel(new System.Guid().ToString());
    }
}
