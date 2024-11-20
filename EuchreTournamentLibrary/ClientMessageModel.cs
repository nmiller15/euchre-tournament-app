namespace EuchreTournamentLibrary;

public class ClientMessageModel : MessageModel
{
    public string RoomCode { get; set; }

    public ClientMessageModel()
    {
        Type = "Unknown";
        RoomCode = "UNWN";
        Message = "None";
    }
    
    public ClientMessageModel(string type, string roomCode, string message)
    {
        Type = type;
        RoomCode = roomCode;
        Message = message;
    }
    
    public ClientMessageModel(string type, string roomCode, string message, RoomModel payload)
    {
        Type = type;
        RoomCode = roomCode;
        Message = message;
        RoomPayload = payload;
    }
    
    public ClientMessageModel(string type, string roomCode, string message, UserModel payload)
    {
        Type = type;
        RoomCode = roomCode;
        Message = message;
        UserPayload = payload;
    }
    
    public ClientMessageModel(string type, string roomCode, string message, RoundModel payload)
    {
        Type = type;
        RoomCode = roomCode;
        Message = message;
        RoundPayload = payload;
    }

    public ClientMessageModel(string type, string roomCode, string message, TeamModel payload)
    {
        Type = type;
        RoomCode = roomCode;
        Message = message;
        TeamPayload = payload;
    }
}