namespace EuchreTournamentLibrary;

public class TeamModel
{
    public int RoundNumber { get; set; }
    public List<PlayerModel> Players { get; set; }
    public int Score { get; set; }

    public TeamModel(int roundNumber, List<PlayerModel> players)
    {
        RoundNumber = roundNumber;
        Players = players;
    }
}