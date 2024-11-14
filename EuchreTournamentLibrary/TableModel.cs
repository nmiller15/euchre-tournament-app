namespace EuchreTournamentLibrary;

public class TableModel
{
    public int Number { get; set; }
    public RoundModel Round { get; set; }
    public List<TeamModel> Teams { get; set; }

    public TableModel(int number, RoundModel round, List<TeamModel> teams)
    {
        Number = number;
        Round = round;
        Teams = teams;
    }
}