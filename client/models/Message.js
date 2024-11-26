export default class Message {
  Type = "";
  RoomCode = "";
  Payload = {};

  constructor(type, roomCode, payload) {
    this.Type = type;
    this.RoomCode = roomCode;
    if (payload) {
      this.Payload = payload;
    }
  }

  JString() {
    return JSON.stringify(this);
  }
}
