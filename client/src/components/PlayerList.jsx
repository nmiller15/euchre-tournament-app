function PlayerList({ players }) {
  return (
    <div className="bg-white rounded-lg border-2 w-80 mt-4 p-2">
      <h2 className="text-xs text-slate-400">Waiting for players to join...</h2>
      <ul>
        {players.map((player) => {
          const { username, uuid, isReady } = player;
          return (
            <li key={uuid}>
              {username}
              <i
                className={`${
                  isReady
                    ? "iconoir-check text-green-500"
                    : "iconoir-xmark text-red-500"
                } ml-1 translate-y-1`}
              ></i>
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default PlayerList;
