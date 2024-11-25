namespace EuchreTournamentLibrary;

public class RoundEntryModel
{
    /// <summary>
    /// Represents the player who submitted the entry.
    /// </summary>
    public string ReportingPlayerGuid { get; set; }
    
    /// <summary>
    /// Represents the team on behalf of which the entry was submitted.
    /// </summary>
    public TeamModel ReportingTeam { get; set; }
    
    /// <summary>
    /// Represents the round to which the entry belongs.
    /// </summary>
    public int RoundNumber { get; set; }
    
    /// <summary>
    /// Represents the points earned by the ReportingTeam in the RoundNumber.
    /// </summary>
    public int Score { get; set; }
    
    /// <summary>
    /// Represents the loners successfully completed by the ReportingPlayer in the RoundNumber.
    /// </summary>
    public int Loners { get; set; }

    /// <summary>
    /// Instantiates a new round entry from a player, team, round number and score.
    /// </summary>
    /// <param name="reportingPlayer">Represents the player who submitted the entry.</param>
    /// <param name="reportingTeam">Represents the team on behalf of which the entry was submitted.</param>
    /// <param name="roundNumber">Represents the round to which the entry belongs.</param>
    /// <param name="score">Represents the points earned by the ReportingTeam in the RoundNumber.</param>
    public RoundEntryModel(string reportingPlayerGuid, int roundNumber, int score, int loners)
    {
        ReportingPlayerGuid = reportingPlayerGuid;
        RoundNumber = roundNumber;
        Score = score;
        Loners = loners;
    }

    public void UpdateEntryScore(int score)
    {
        Score = score;
    }

    public void UpdateEntryLoners(int loners)
    {
        Loners = loners;
    }
}