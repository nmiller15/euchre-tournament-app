namespace EuchreTournamentLibrary;

public class RoundModel
{
    public int RoundNumber { get; set; }
    public List<PlayerModel> PlayersSittingOut { get; set; }
    public List<TableModel> RoundTables { get; set; }
    public Dictionary<TeamModel, int> RoundScores { get; set; }

    public RoundModel(int roundNumber, List<TableModel> roundTables, List<PlayerModel> playersSittingOut)
    {
        RoundNumber = roundNumber;
        RoundTables = roundTables;
        PlayersSittingOut = playersSittingOut;
        RoundScores = new Dictionary<TeamModel, int>();
    }
    

}