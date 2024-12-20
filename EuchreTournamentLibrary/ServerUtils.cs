using System.Web;
using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EuchreTournamentLibrary;

public static class ServerUtils
{
    public static string ParseValueFromParamsKey(IWebSocketConnection connection, string param)
    {
        var path = connection.ConnectionInfo.Path;
        var queryString = path.Split('?')[1];
        var paramsCollection = HttpUtility.ParseQueryString(queryString);
        var value = paramsCollection[param];

        return value;
    }
    
    public static bool IsValidJson(string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) return false;
        strInput = strInput.Trim();
        if ((strInput.StartsWith('{') && strInput.EndsWith('}') ||
             strInput.StartsWith('[') && strInput.EndsWith(']')))
        {
            try
            {
                JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
        
        return false;
    }
    
    public static ClientMessageModel ParseClientMessage(string jsonMessage)
    {
        var messageObject = JsonConvert.DeserializeObject<JObject>(jsonMessage);
        var clientMessage = new ClientMessageModel();
        foreach (var property in messageObject.Properties())
        {
            string key = property.Name;
            var propInfo = clientMessage.GetType().GetProperty(key);

            if (propInfo == null || !propInfo.CanWrite) continue;
            if (propInfo.PropertyType == typeof(string))
            {
                var value = property.Value.ToString();
                propInfo.SetValue(clientMessage, value);
                    
            }
            else
            {
                var deserializedValue = property.Value.ToObject(propInfo.PropertyType);
                propInfo.SetValue(clientMessage, deserializedValue);
            }
        }

        return clientMessage;
    }
}