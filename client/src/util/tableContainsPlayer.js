export const tableContainsPlayer = (table, userGuid) => {
  const players = [];

  table.Teams.forEach((team) => {
    team.Players.forEach((player) => {
      players.push(player);
    });
  });

  return players.some((player) => player.Guid == userGuid);
};
