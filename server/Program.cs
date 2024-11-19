using System.Text.Json;using System.Threading.Tasks.Dataflow;
using EuchreTournamentLibrary;
using Fleck;
using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Internal;

var serverAddress = "127.0.0.1";
var port = "8080";
// var server = new WebSocketServer($"ws://{serverAddress}:{port}/");

var server = new WebSocketServerModel(serverAddress, port);

// var wsConnections = new Dictionary<string, IWebSocketConnection>();
// var users = new Dictionary<string, UserModel>();


// server.Start(ws =>
// {
//     ws.OnOpen = () =>
//     {
//         // ws://127.0.0.1?username=nolan
//         // Get the url for the connection
//         var path = ws.ConnectionInfo.Path;
//         
//         // Parse username from the url query parameters
//         var queryString = path.Split('?')[1];
//         var paramsCollection = HttpUtility.ParseQueryString(queryString);
//         var username = paramsCollection["username"];
//         
//         // Generate a new user from the UserModel
//         // If the wsConnections list is empty, then make a host
//         var guid = System.Guid.NewGuid().ToString();
//         UserModel user = new UserModel(newGuid, username, wsConnections.Count > 0 ? false : true);
//         
//         // Push User and Connection objects to Dictionaries
//         users.Add(guid, user);
//         wsConnections.Add(guid, ws);
//         
//         Console.WriteLine($"New Connection at {TimeProvider.System.GetUtcNow()}: User {username}");
//         
//         var userJson = JsonSerializer.Serialize(user);
//         ws.Send(userJson);
//         
//         ws.OnClose = () =>
//         {
//             wsConnections.Remove(guid);
//
//             var wsConnectionsKeys = wsConnections.Keys.ToList();
//             foreach (var key in wsConnectionsKeys)
//             {
//                 wsConnections[key].Send($"User {username} has disconnected.");
//             }
//             
//             users.Remove(guid);
//             Console.WriteLine("Disconnected");
//         };
//         ws.OnMessage = message =>
//         {
//             Console.WriteLine($"Received: {message}");
//         };
//     };
// });

Console.WriteLine($"The server is started at ws://{serverAddress}:{port}/");
Console.ReadLine();

