using Fleck;

string serverAddress = "127.0.0.1";
var port = 8080;
var server = new WebSocketServer($"ws://{serverAddress}:{port}/");

var wsConnections = new List<IWebSocketConnection>();
var users = new List<>();

server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        Console.WriteLine($"Connected");
        wsConnections.Add(ws);
    };
    ws.OnClose = () =>
    {
        Console.WriteLine("Disconnected");
    };
    ws.OnMessage = message =>
    {
        Console.WriteLine($"Received: {message}");
        foreach (var connection in wsConnections)
        {
            connection.Send(message);
        }
    };
});

Console.WriteLine($"The server is started at ws://{serverAddress}:{port}/");
Console.ReadLine();

