import PlayerList from "./components/PlayerList";
import Message from "../models/Message";

function Lobby({ room, user, send }) {
  const { RoomCode, Users } = room;

  const handleStart = () => {
    const message = new Message("Room:Start", room.RoomCode);
    send(message.JString());
  };

  const handleReadyUp = () => {
    const message = new Message(
      !user.IsReady ? "User:ReadyUp" : "User:Unready",
      room.RoomCode,
    );
    send(message.JString());
  };

  return (
    <div className="">
      <div>
        <h1 className="mt-4 text-center text-xl">Euchre Tournament</h1>
        <p className="text-center text-sm">Pre-game Lobby</p>
      </div>
      <div className="mx-auto mt-4 w-24 rounded-lg border-2 bg-white py-2">
        <p className="rounded-md text-center text-2xl">{RoomCode}</p>
      </div>
      <PlayerList users={Users} />
      {user.IsHost && Object.keys(room.Users).length < 8 ? (
        <button className="mx-auto mt-4 w-full rounded-lg bg-slate-400 px-2 py-3 text-center">
          Waiting for players...
        </button>
      ) : user.IsHost && room.IsReady ? (
        <button
          onClick={handleStart}
          className="mx-auto mt-4 w-full rounded-lg bg-green-500 px-2 py-[0.65rem] text-center text-lg"
        >
          Start Tournament
        </button>
      ) : user.IsHost ? (
        <button className="mx-auto mt-4 w-full rounded-lg bg-slate-400 px-2 py-3 text-center">
          Waiting for players to ready up...
        </button>
      ) : (
        <button className="mx-auto mt-4 w-full rounded-lg bg-slate-400 px-2 py-3 text-center">
          Waiting for host to start...
        </button>
      )}
      <button
        onClick={handleReadyUp}
        className={`mx-auto mt-4 w-full rounded-lg ${!user.IsReady ? "bg-green-500" : "bg-slate-400"} px-2 py-3 text-center`}
      >
        {!user.IsReady ? "Ready Up" : "Unready"}
      </button>
    </div>
  );
}

export default Lobby;
