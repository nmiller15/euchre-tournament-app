namespace EuchreTournamentLibrary;

public class MessageModel
{
    /// <summary>
    /// Represents the type of message that will be sent to the client. This is used by the client to make decisions
    /// on what to do with the incoming message.
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// Represents a text string describing the content of the payload, or to perform string interactions with the
    /// client.
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// A payload option for a message, contains a RoomModel.
    /// </summary>
    public RoomModel? RoomPayload { get; set; }
    
    /// <summary>
    /// A payload option for a message, contains a UserModel.
    /// </summary>
    public UserModel? UserPayload { get; set; }
    
    /// <summary>
    /// A payload option for a message, contains a RoundModel.
    /// </summary>
    public RoundModel? RoundPayload { get; set; }

    
    /// <summary>
    /// A payload option for a message, contains a TeamModel.
    /// </summary>
    public TeamModel? TeamPayload { get; set; }

    public MessageModel()
    {
        Type = "Unknown";
        Message = "None";
    }

    public MessageModel(string type, string message)
    {
        Type = type;
        Message = message;
    }
    
    public MessageModel(string type, string message, RoomModel payload)
    {
        Type = type;
        Message = message;
        RoomPayload = payload;
    }
    
    public MessageModel(string type, string message, UserModel payload)
    {
        Type = type;
        Message = message;
        UserPayload = payload;
    }
    
    public MessageModel(string type, string message, RoundModel payload)
    {
        Type = type;
        Message = message;
        RoundPayload = payload;
    }

    public MessageModel(string type, string message, TeamModel payload)
    {
        Type = type;
        Message = message;
        TeamPayload = payload;
    }
}