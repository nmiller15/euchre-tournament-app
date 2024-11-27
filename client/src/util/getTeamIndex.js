export const getTeamIndex = (round, userGuid) => {
  const tables = round.RoundTables;

  for (let i = 0; i < tables.length; i++) {
    const players = tables[i].Players;

    for (let j = 0; j < players.length; j++) {
      const player = players[j];
      if (player.Guid == userGuid) return j;
    }
  }

  return -1;
};
