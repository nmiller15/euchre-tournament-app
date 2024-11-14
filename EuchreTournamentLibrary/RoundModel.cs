namespace EuchreTournamentLibrary;

public class RoundModel
{
    public int RoundNumber { get; set; }
    public List<PlayerModel> PlayersSittingOut { get; set; }
    public List<TableModel> RoundTables { get; set; }
    public Dictionary<RoundEntryModel, int> RoundScoreEntries { get; set; }

    public RoundModel(int roundNumber, List<TableModel> roundTables, List<PlayerModel> playersSittingOut)
    {
        RoundNumber = roundNumber;
        RoundTables = roundTables;
        PlayersSittingOut = playersSittingOut;
        RoundScoreEntries = new Dictionary<RoundEntryModel, int>();
    }
    

}