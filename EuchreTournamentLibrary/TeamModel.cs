using Newtonsoft.Json;

namespace EuchreTournamentLibrary;

public class TeamModel
{
    /// <summary>
    /// Represents the round to which this team belongs.
    /// Teams are transient and only exist within one round.
    /// </summary>
    public int RoundNumber { get; set; }
    
    public string Guid { get; private set; }
    
    public int TableNumber { get; set; }
    
    /// <summary>
    /// Represents the two players that are associated with a given team.
    /// </summary>
    public List<UserModel> Players { get; set; }
    
    /// <summary>
    /// Represents the points that are scored in the round by the given team.
    /// This is assigned after the RoundEntry is submitted by one of the team members.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Instatiates a team given a round and a list of players.
    /// </summary>
    /// <param name="roundNumber">Represents the round to which this team belongs</param>
    /// <param name="players">Represents the two players that are associated with a given team.</param>
    public TeamModel(string guid, int roundNumber, List<UserModel> players)
    {
        RoundNumber = roundNumber;
        Players = players;
        Guid = guid;
    }

    public void UpdateScore(int score)
    {
        Score = score;
        Players[0].UpdateRoundEntry(RoundNumber, score);
        Players[1].UpdateRoundEntry(RoundNumber, score);
    }

    public void BroadcastToTeam(MessageModel message)
    {
        foreach (var player in Players)
        {
            var jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
            player.Connection.Send(jsonMessage);
        }
    }
}