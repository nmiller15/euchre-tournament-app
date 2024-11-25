import PlayerList from "./components/PlayerList";

function Lobby({ room }) {
  const { RoomCode, Users } = room;

  const handleStart = () => {
    console.log("start");
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
      {/* <button className="mt-4 text-center bg-slate-400 rounded-lg px-2 py-3 mx-auto w-full">
        Waiting for host to start...
      </button> */}
      {/* <button className="mt-4 text-center bg-slate-400 rounded-lg px-2 py-3 mx-auto w-full">
        Waiting for players to ready up...
      </button> */}
      <button
        onClick={handleStart}
        className="mx-auto mt-4 w-full rounded-lg bg-green-500 px-2 py-[0.65rem] text-center text-lg"
      >
        Start Tournament
      </button>
    </div>
  );
}

export default Lobby;
