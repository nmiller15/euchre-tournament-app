namespace EuchreTournamentLibrary;

public class RoundEntryModel
{
    public PlayerModel ReportingPlayer { get; set; }
    public TeamModel ReportingTeam { get; set; }
    public int RoundNumber { get; set; }
    public int Score { get; set; }

    public RoundEntryModel(PlayerModel reportingPlayer, TeamModel reportingTeam, int roundNumber, int score)
    {
        ReportingPlayer = reportingPlayer;
        ReportingTeam = reportingTeam;
        RoundNumber = roundNumber;
        Score = score;
    }
}