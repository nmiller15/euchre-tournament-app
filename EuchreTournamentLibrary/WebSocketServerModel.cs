using System.Text.Json;
using System.Web;
using Fleck;
namespace EuchreTournamentLibrary;

public class WebSocketServerModel
{
    /// <summary>
    /// Represents the server object created by the instatiation of the WebSocketServer class.
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
        var username = ParseUsername(connection);
        var isFirstConnection = Connections.Keys.ToList().Count == 0;
        var user = new UserModel(guid, username, isFirstConnection);
        
        AddConnection(guid, connection);
        AddUser(guid, user);
        Console.WriteLine($"User {username} connected.");
        
        var userJson = JsonSerializer.Serialize(user);
        connection.Send(userJson);
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

    private string ParseUsername(IWebSocketConnection connection)
    {
        var path = connection.ConnectionInfo.Path;
        var queryString = path.Split('?')[1];
        var paramsCollection = HttpUtility.ParseQueryString(queryString);
        var username = paramsCollection["username"];

        return username;
    }

    public void Broadcast(string message)
    {
        var connectionKeys = Connections.Keys.ToList();
        foreach (var key in connectionKeys)
        {
            Connections[key].Send(message);
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