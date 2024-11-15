import PlayerList from "./components/PlayerList";

function Lobby({ room }) {
  const { roomCode, players } = room;

  const handleStart = () => {
    console.log("start");
  };

  return (
    <div className="">
      <div>
        <h1 className="text-center text-xl mt-4">Euchre Tournament</h1>
        <p className="text-sm text-center">Pre-game Lobby</p>
      </div>
      <div className="bg-white rounded-lg py-2 border-2 mt-4 w-24 mx-auto">
        <p className="rounded-md text-2xl text-center">{roomCode}</p>
      </div>
      <PlayerList players={players} />
      {/* <button className="mt-4 text-center bg-slate-400 rounded-lg px-2 py-3 mx-auto w-full">
        Waiting for host to start...
      </button> */}
      {/* <button className="mt-4 text-center bg-slate-400 rounded-lg px-2 py-3 mx-auto w-full">
        Waiting for players to ready up...
      </button> */}
      <button
        onClick={handleStart}
        className="mt-4 text-center text-lg bg-green-500 rounded-lg px-2 py-[0.65rem] mx-auto w-full"
      >
        Start Tournament
      </button>
    </div>
  );
}

export default Lobby;
