using System.Text.Json;
using System.Web;
using Fleck;
using Newtonsoft.Json;

namespace EuchreTournamentLibrary;

public class WebSocketServerModel
{
    /// <summary>
    /// Represents the server object created by the instantiation of the WebSocketServer class.
    /// </summary>
    private WebSocketServer Server { get; set; }
    
    /// <summary>
    /// Represents the address that will host the WebSocketServer.
    /// </summary>
    private string ServerAddress { get; set; }
    
    /// <summary>
    /// Represents the port that will host the WebSocketServer.
    /// </summary>
    private string Port { get; set; }
    
    /// <summary>
    /// Represents all active connection objects attached to the server, defined by their GUID.
    /// </summary>
    private Dictionary<string, IWebSocketConnection> Connections { get;  set; }
    
    /// <summary>
    /// Represents all active Users with connection objects, defined by their GUID.
    /// </summary>
    private Dictionary<string, UserModel> Users { get;  set; }
    
    /// <summary>
    /// Represents all active rooms, defined by their join code.
    /// </summary>
    private Dictionary<string, RoomModel> Rooms { get; set; }
    
    /// <summary>
    /// The constructor for the WebSocketServer. Instantiates a WebSocketServer
    /// </summary>
    /// <param name="serverAddress">Represents the address that will host the WebSocketServer.</param>
    /// <param name="port">Represents the port that will host the WebSocketServer.</param>
    public WebSocketServerModel(string serverAddress, string port)
    {
        ServerAddress = serverAddress;
        Port = port;
        Connections = new Dictionary<string, IWebSocketConnection>();
        Users = new Dictionary<string, UserModel>();
        Rooms = new Dictionary<string, RoomModel>();

        Server = new WebSocketServer($"ws://{ServerAddress}:{Port}");
        Server.Start(( IWebSocketConnection connection) =>
        {
            connection.OnOpen = () =>
            {
                var guid = System.Guid.NewGuid().ToString();
                
                HandleOpen(guid, connection);
                connection.OnClose = () => HandleClose(Users[guid], Connections[guid]);
                connection.OnMessage = message => HandleMessage(Users[guid], Connections[guid], message);
            };
        });
    }

    private void HandleOpen(string guid, IWebSocketConnection connection)
    {
        var username = ParseValueFromParamsKey(connection, "username");
        if (string.IsNullOrWhiteSpace(username))
        {
            connection.Send(
                JsonConvert.SerializeObject(
                    new MessageModel("Error", "Invalid or missing Username")
                )
            );
        }
        var createRoom = ParseValueFromParamsKey(connection, "createRoom") == "true";
        var user = new UserModel(guid, username, createRoom, connection);
        
        if (createRoom)
        {
            // Create a new room with the user and add to Server's room list.
            var room = new RoomModel(user);
            user.SendMessageToUser(new MessageModel(
                "Room:Create", 
                $"A new room has been created with code {room.RoomCode}.",
                room
            ));
                
            Rooms.Add(room.RoomCode, room);
        }
        else
        {
            // User is attempting to join room.
            var roomCode = ParseValueFromParamsKey(connection, "roomCode");
            if (string.IsNullOrWhiteSpace(roomCode))
            {
                user.SendMessageToUser(new MessageModel("Error", "Missing room code."));
            }
            var roomFound = Rooms.TryGetValue(roomCode, out var room);
            if (!roomFound)
            {
                user.SendMessageToUser(new MessageModel("Error", "No room with this code found."));
            }
            else
            {
                room.AddUser(user.Guid, user);
                room.BroadcastToRoom(new MessageModel(
                    "Room:Join",
                    $"{user.Username} has joined the room.",
                    room
                ));
            }
        }
        
        AddConnection(guid, connection);
        AddUser(guid, user);
        
        user.SendMessageToUser(new MessageModel(
            "User:Create",
            $"Your user has been created with username {user.Username}.",
            user
        ));
    }

    private void HandleClose(UserModel user, IWebSocketConnection connection)
    {
        RemoveConnection(user.Guid);
        RemoveUser(user.Guid);
        
        Broadcast($"User {user.Username} has disconnected.");
        Console.WriteLine($"User {user.Username} has disconnected.");
    }

    private void HandleMessage(UserModel user, IWebSocketConnection connection, string message)
    {
        Console.WriteLine($"Received message: {message}");
        connection.Send($"Hello {user.Username}");
        Broadcast($"{user.Username}: {message}");
    }
    
    private void HandleMessage(UserModel user, IWebSocketConnection connection, ClientMessageModel message)
    {
        Console.WriteLine($"Received message from {user.Username} in room {message.RoomCode}");
        
        // Identify the relevant room
        var roomFound = Rooms.TryGetValue(message.RoomCode, out var room);
        if (!roomFound)
        {
            user.SendMessageToUser(new MessageModel("Error", "No room with matching code found."));
            return;
        }
        
        switch (message.Type)
        {
            case "User:ReadyUp":
                user.ReadyUpUser();
                room.Users[user.Guid].ReadyUpUser();
                room.BroadcastToRoom(new MessageModel("User:ReadyUp", $"User {user.Username} is ready.", room));
                break;
            case "User:Unready":
                user.UnreadyUser();
                room.Users[user.Guid].UnreadyUser();
                room.BroadcastToRoom(new MessageModel("User:Unready", $"User {user.Username} is not ready.", room));
                break;
            default:
                user.SendMessageToUser(new MessageModel("Error", "Unknown message type."));
                break;
        }
    }

    static string ParseValueFromParamsKey(IWebSocketConnection connection, string param)
    {
        var path = connection.ConnectionInfo.Path;
        var queryString = path.Split('?')[1];
        var paramsCollection = HttpUtility.ParseQueryString(queryString);
        var value = paramsCollection[param];

        return value;
    }

    public void Broadcast(string message)
    {
        var jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
        foreach (var guid in Users.Keys)
        {
            Users[guid].Connection.Send(jsonMessage);
        }
    }

    public void Broadcast(MessageModel message)
    {
        var jsonMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
        foreach (var guid in Users.Keys)
        {
            Users[guid].Connection.Send(jsonMessage);
        }
    }

    private void AddUser(string guid, UserModel user)
    {
        Users.Add(guid, user);
    }

    private void RemoveUser(string guid)
    {
        Users.Remove(guid);
    }

    private void AddConnection(string guid, IWebSocketConnection connection)
    {
        Connections.Add(guid, connection);
    }

    private void RemoveConnection(string guid)
    {
        Connections.Remove(guid);
    }

}