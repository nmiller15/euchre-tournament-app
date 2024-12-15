export const handleMessage = (message, room, setRoom, user, setUser) => {
  console.log(message);
  switch (message.Type) {
    case "Room:Join": {
      setRoom(message.RoomPayload);
      break;
    }
    case "User:ReadyUp": {
      setRoom(message.RoomPayload);
      setUser(message.RoomPayload.Users[user.Guid]);
      break;
    }
    case "User:Unready": {
      setRoom(message.RoomPayload);
      setUser(message.RoomPayload.Users[user.Guid]);
      break;
    }
    case "Room:Schedule": {
      setRoom(message.RoomPayload);
      break;
    }
    case "Room:UpdatePoints": {
      setRoom(message.RoomPayload);
      setUser(message.RoomPayload.Users[user.Guid]);
      break;
    }
    case "Room:UpdateUserLoners": {
      setRoom(message.RoomPayload);
      setUser(message.RoomPayload.Users[user.Guid]);
      break;
    }
    case "Round:SubmitEntry": {
      setRoom(message.RoomPayload);
      setUser(message.RoomPayload.Users[user.Guid]);
      break;
    }
    case "Room:NewRound": {
      setRoom(message.RoomPayload);
      setUser(message.RoomPayload.Users[user.Guid]);
      break;
    }
    default: {
      break;
    }
  }
};
