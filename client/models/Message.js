export default class Message {
  Type = "";
  RoomCode = "";
  TeamPayload = {};
  RoundEntryPayload = {};

  constructor(type, roomCode, payload, payloadType) {
    this.Type = type;
    this.RoomCode = roomCode;
    switch (payloadType) {
      case "TeamPayload": {
        this.TeamPayload = payload;
        break;
      }
      case "RoundEntryPayload": {
        this.RoundEntryPayload = payload;
        break;
      }
    }
  }

  JString() {
    return JSON.stringify(this);
  }
}
