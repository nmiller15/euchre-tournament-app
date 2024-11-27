export const getPartner = (team, userGuid) => {
  // const tables = round.RoundTables;
  // // const teams = tables.Teams;
  // let teams = [];

  // tables.forEach((table) => {
  //   table.Teams.forEach((team) => {
  //     teams.push(team);
  //   });
  // });

  // for (let i = 0; i < teams.length; i++) {
  //   if (teams[i].Players.some((player) => player.Guid == userGuid)) {
  //     return teams[i].Players[0].Guid == userGuid
  //       ? teams[i].Players[1]
  //       : teams[i].Players[0];
  //   }
  // }

  return team.Players[0].Guid == userGuid ? team.Players[1] : team.Players[0];
};
