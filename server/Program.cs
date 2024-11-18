using Fleck;

string serverAddress = "127.0.0.1";
var port = 8080;
var server = new WebSocketServer($"ws://{serverAddress}:{port}/");

server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        Console.WriteLine($"Connected");
    };
    ws.OnClose = () =>
    {
        Console.WriteLine("Disconnected");
    };
    ws.OnMessage = message =>
    {
        Console.WriteLine($"Received: {message}");
    };
});

Console.WriteLine($"The server is started at ws://{serverAddress}:{port}/");
Console.ReadLine();

