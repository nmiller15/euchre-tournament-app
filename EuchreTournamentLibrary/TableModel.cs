namespace EuchreTournamentLibrary;

public class TableModel
{
    /// <summary>
    /// Refers to a specific location for the table.
    /// Identifies where the players' match is located.
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// Represents the round to which this table belongs.
    /// Each table can only belong to one round.
    /// </summary>
    public RoundModel Round { get; set; }
    
    /// <summary>
    /// Represents the two teams that will play at this table.
    /// Teams are transient and only exist within their current round.
    /// </summary>
    public List<TeamModel> Teams { get; set; }
    
    /// <summary>
    /// Instantiates a new table given an identifying number, a round and two teams.
    /// </summary>
    /// <param name="number">Identifies where the players' match is located.</param>
    /// <param name="round">Represents the round to which this table belongs.</param>
    /// <param name="teams">Represents the two teams that will play at this table.</param>
    public TableModel(int number, RoundModel round, List<TeamModel> teams)
    {
        Number = number;
        Round = round;
        Teams = teams;
    }
}