namespace EuchreTournamentLibrary;

public class MessageModel
{
    public string Type { get; set; }
    
    public string Message { get; set; }
    
    public string? StringPayload { get; set; }
    
    public RoomModel? RoomPayload { get; set; }
    
    public UserModel? UserPayload { get; set; }
    
    public RoundModel? RoundPayload { get; set; }
    
    public TeamModel? TeamPayload { get; set; }

    public MessageModel(string type, string message, string payload)
    {
        Type = type;
        Message = message;
        StringPayload = payload;
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