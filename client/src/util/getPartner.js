export const getPartner = (round, userGuid) => {
  const tables = round.RoundTables;
  console.log(tables);
  // const teams = tables.Teams;
  let teams = [];

  tables.forEach((table) => {
    table.Teams.forEach((team) => {
      teams.push(team);
    });
  });

  for (let i = 0; i < teams.length; i++) {
    if (teams[i].Players.some((player) => player.Guid == userGuid)) {
      return teams[i].Players[0].Guid == userGuid
        ? teams[i].Players[1]
        : teams[i].Players[0];
    }
  }

  return;
};
