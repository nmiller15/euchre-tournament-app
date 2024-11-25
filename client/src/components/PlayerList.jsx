function PlayerList({ users }) {
  return (
    users != null && (
      <div className="mt-4 w-80 rounded-lg border-2 bg-white p-2">
        <h2 className="text-xs text-slate-400">
          Waiting for players to join...
        </h2>
        <ul>
          {Object.keys(users).map((guid) => {
            const { Username, Guid, IsReady } = users[guid];
            return (
              <li key={Guid}>
                {Username}
                <i
                  className={`${
                    IsReady
                      ? "iconoir-check text-green-500"
                      : "iconoir-xmark text-red-500"
                  } ml-1 translate-y-1`}
                ></i>
              </li>
            );
          })}
        </ul>
      </div>
    )
  );
}

export default PlayerList;
