namespace EuchreTournamentLibrary;

public class RoundModel
{
    /// <summary>
    /// Represents the order in which the round is placed within the room schedule.
    /// </summary>
    public int RoundNumber { get; set; }
    
    /// <summary>
    /// A list of players who are not at a table in this round.
    /// </summary>
    public List<UserModel> PlayersSittingOut { get; set; }
    
    /// <summary>
    /// A list of tables associated with this round.
    /// Table objects are only associated with a single round.
    /// </summary>
    public List<TableModel> RoundTables { get; set; }
    
    /// <summary>
    /// A list of entries submitted by players/teams containing score information.
    /// </summary>
    public List<RoundEntryModel> RoundScoreEntries { get; set; }

    /// <summary>
    /// Instantiates a round object given an identifying number, tables, and players not included.
    /// </summary>
    /// <param name="roundNumber">Represents the order in which the round is placed within the room schedule.</param>
    /// <param name="roundTables">A list of entries submitted by players/teams containing score information.</param>
    /// <param name="playersSittingOut">A list of players who are not at a table in this round.</param>
    public RoundModel(int roundNumber, List<TableModel> roundTables, List<UserModel> playersSittingOut)
    {
        RoundNumber = roundNumber;
        RoundTables = roundTables;
        PlayersSittingOut = playersSittingOut;
        RoundScoreEntries = new List<RoundEntryModel>();
    }
    

}