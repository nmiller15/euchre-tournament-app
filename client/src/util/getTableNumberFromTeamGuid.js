export const getTableNumberFromTeamGuid = (round, team) => {
  const { RoundTables } = round;
  const { Guid } = team;

  for (let i = 0; i < RoundTables.length; i++) {
    const roundTable = RoundTables[i];
    const roundTeams = roundTable.Teams;

    for (let j = 0; j < roundTeams.length; j++) {
      const roundTeam = roundTeams[j];
      if (roundTeam.Guid == Guid) return roundTable.Number;
    }
  }
  return;
};
