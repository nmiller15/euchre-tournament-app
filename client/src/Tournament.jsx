import { useState, useEffect } from "react";
import { tableContainsPlayer } from "./util/tableContainsPlayer";
import { getPartner } from "./util/getPartner";
import { getTeam } from "./util/getTeam";
import Message from "../models/Message";

//TODO: Divide this "component" into sub-components. File is too large.

function Tournament({ room, user, send }) {
  // All tournament data is from the room object
  // ===========================================
  const { Schedule, CurrentRound } = room;

  // State of the data on the screen is determined by which round is in state.
  // =========================================================================
  const [displayRound, setDisplayRound] = useState(
    Schedule.find((round) => round.RoundNumber == CurrentRound),
  );
  const [sittingOut, setSittingOut] = useState(() => {
    if (!Schedule[CurrentRound].PlayersSittingOut) return false;
    return Schedule[CurrentRound].PlayersSittingOut.some(
      (player) => player.Guid == user.Guid,
    );
  });
  const [roundEntry, setRoundEntry] = useState(() => {
    if (sittingOut) return { Loners: 0 };
    return user.RoundEntries[displayRound.RoundNumber - 1]
      ? user.RoundEntries[displayRound.RoundNumber - 1]
      : { Loners: 0 };
  });
  const [entrySubmitted, setEntrySubmitted] = useState(false);
  const [currentTeam, setCurrentTeam] = useState(() => {
    if (sittingOut) return { Score: 0 };
    return getTeam(displayRound, user.Guid);
  });
  const [currentPartner, setCurrentPartner] = useState(() => {
    if (sittingOut) return null;
    return getPartner(currentTeam, user.Guid);
  });

  // Round changer interface logic
  // =============================
  const handleSeePrevRound = () => {
    const roundNumber = displayRound.RoundNumber;
    if (roundNumber == 1) return;
    const prevRoundObj = Schedule.find(
      (round) => round.RoundNumber == roundNumber - 1,
    );
    setDisplayRound(prevRoundObj);
    setSittingOut(() => {
      const isSittingOut = prevRoundObj.PlayersSittingOut.some(
        (player) => player.Guid == user.Guid,
      );
      if (isSittingOut) {
        setCurrentTeam({ Score: 0 });
        setCurrentPartner(null);
        setRoundEntry({ Loners: 0 });
      } else {
        const team = getTeam(prevRoundObj, user.Guid);
        const partner = getPartner(team, user.Guid);
        const entry = user.RoundEntries[prevRoundObj.RoundNumber - 1]
          ? user.RoundEntries[prevRoundObj.RoundNumber - 1]
          : { Loners: 0 };
        setCurrentTeam(team);
        setCurrentPartner(partner);
        setRoundEntry(entry);
      }
      return isSittingOut;
    });
  };

  const handleSeeNextRound = () => {
    const roundNumber = displayRound.RoundNumber;
    if (roundNumber == Schedule.length) return;
    const nextRoundObj = Schedule.find(
      (round) => round.RoundNumber == roundNumber + 1,
    );
    setDisplayRound(nextRoundObj);
    setSittingOut(() => {
      const isSittingOut = nextRoundObj.PlayersSittingOut.some(
        (player) => player.Guid == user.Guid,
      );
      if (isSittingOut) {
        setCurrentTeam({ Score: 0 });
        setCurrentPartner(null);
        setRoundEntry({ Loners: 0 });
      } else {
        const team = getTeam(nextRoundObj, user.Guid);
        const partner = getPartner(team, user.Guid);
        const entry = user.RoundEntries[nextRoundObj.RoundNumber - 1]
          ? user.RoundEntries[nextRoundObj.RoundNumber - 1]
          : { Loners: 0 };
        setCurrentTeam(team);
        setCurrentPartner(partner);
        setRoundEntry(entry);
      }
      return isSittingOut;
    });
  };
  //##########################################

  // Score Update Logic
  // ==================
  const updateScore = (updatedTeam) => {
    const message = new Message(
      "Team:UpdatePoints",
      room.RoomCode,
      updatedTeam,
      "TeamPayload",
    );
    send(message.JString());
  };

  const updateLoners = (updatedRoundEntry) => {
    const message = new Message(
      "RoundEntry:UpdateUserLoners",
      room.RoomCode,
      updatedRoundEntry,
      "RoundEntryPayload",
    );
    send(message.JString());
  };
  // ##########################################

  const handleSubmit = (e) => {
    e.preventDefault();
    if (sittingOut) return;
    const result = confirm(
      `Click "OK" to submit your score of ${currentTeam.Score} point(s) ${roundEntry.Loners} loner(s).`,
    );
    if (!result) return;
    console.log("Submitted");
    const message = new Message("Round:SubmitEntry", room.RoomCode);
    send(message.JString());
    setEntrySubmitted(true);
  };

  useEffect(() => {
    if (!displayRound) {
      return;
    }
    if (sittingOut) {
      return;
    }
    const team = getTeam(displayRound, user.Guid);
    const partner = getPartner(currentTeam, user.Guid);
    const entry = user.RoundEntries[displayRound.RoundNumber - 1]
      ? user.RoundEntries[displayRound.RoundNumber - 1]
      : { Loners: 0 };
    setCurrentTeam(team);
    setCurrentPartner(partner);
    setRoundEntry(entry);
    setEntrySubmitted(() => {
      console.log("here");
      console.log(displayRound);
      const foundSubmittedEntry = displayRound.RoundScoreEntries.some(
        (entry) => {
          return entry.ReportingPlayerGuid == user.Guid;
        },
      );
      console.log(foundSubmittedEntry);
      return foundSubmittedEntry;
    });
  }, [displayRound]);

  useEffect(() => {
    const updatedRound = Schedule.find(
      (round) => round.RoundNumber == CurrentRound,
    );
    setDisplayRound(updatedRound);
    // setEntrySubmitted(() => {
    // //   const foundSubmittedEntry = updatedRound.RoundScoreEntries.some(
    // //     (entry) => {

    // //       entry.ReportingPlayerGuid == user.Guid;
    // //     },
    // //   );
    // //   return foundSubmittedEntry;
    // // });
    // return true;
  }, [room]);

  return (
    <div className="block w-[360px]">
      <div className="w-full bg-white">
        <h1 className="border-2 border-slate-300 py-1 text-center text-sm">
          Euchre Tournament
        </h1>
      </div>
      {/* Round changer */}
      <div className="mt-6 flex justify-center">
        <i
          className="iconoir-nav-arrow-left mx-14 mt-6 text-3xl"
          onClick={handleSeePrevRound}
        ></i>
        <div className="block">
          <p className="text-center text-xs">Round</p>
          <p className="text-center text-4xl tabular-nums">
            {displayRound.RoundNumber}
          </p>
        </div>
        <i
          className="iconoir-nav-arrow-right mx-14 mt-6 text-3xl"
          onClick={handleSeeNextRound}
        ></i>
      </div>
      {/* Tables */}
      {/* ====== */}
      <div className="mt-6 flex justify-center gap-4">
        {sittingOut ? (
          <p className="mb-28">You&apos;re sitting out this round!</p>
        ) : (
          displayRound.RoundTables.map((table) => {
            return (
              <div
                className={`flex h-8 w-8 flex-col justify-center rounded-full ${tableContainsPlayer(table, user.Guid) ? "bg-black text-white" : "bg-white"}`}
                key={`table${table.Number}`}
              >
                <p className="mt-[2px] h-8 w-8 text-center text-lg">
                  {table.Number}
                </p>
              </div>
            );
          })
        )}
      </div>

      {/* ######################## */}
      {!sittingOut && (
        <>
          <div className="mt-6 text-center">
            <p className="text-xs">You&apos;re playing with</p>
            <p>{currentPartner?.Username}</p>
          </div>
          <div className="mt-6">
            <form onSubmit={handleSubmit}>
              <div className="mx-auto flex h-12 w-3/4 justify-start align-middle">
                <p className="w-[120px] text-xl font-semibold">Points</p>
                <div>
                  <input
                    type="text"
                    value={currentTeam?.Score}
                    className="mr-8 h-10 w-10 rounded-md border-2 border-slate-600 text-center text-lg font-semibold"
                    onChange={(e) => {
                      if (sittingOut) return;
                      console.log("executing at input");
                      setCurrentTeam((prev) => {
                        return { ...prev, Score: e.target.value };
                      });
                    }}
                  />
                  <button
                    type="button"
                    className="mr-2 h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                    onClick={() => {
                      if (sittingOut) return;
                      console.log("executing");
                      setCurrentTeam((prev) => {
                        if (prev.Score == 0) return prev;
                        const newTeam = { ...prev, Score: prev.Score - 1 };
                        updateScore(newTeam);
                        return newTeam;
                      });
                    }}
                  >
                    -
                  </button>
                  <button
                    type="button"
                    className="h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                    onClick={() => {
                      if (sittingOut) return;
                      console.log("executing");
                      setCurrentTeam((prev) => {
                        const newTeam = { ...prev, Score: prev.Score + 1 };
                        updateScore(newTeam);
                        return newTeam;
                      });
                    }}
                  >
                    +
                  </button>
                </div>
              </div>
              <div className="mx-auto flex h-12 w-3/4 justify-start align-middle">
                <p className="w-[120px] text-xl font-semibold">Loners</p>
                <div>
                  <input
                    type="text"
                    value={roundEntry?.Loners}
                    className="mr-8 h-10 w-10 rounded-md border-2 border-slate-600 text-center text-lg font-semibold"
                    onChange={(e) => {
                      if (sittingOut) return;
                      console.log("executing at input");
                      setRoundEntry((prev) => {
                        return {
                          ...prev,
                          Loners: e.target.value,
                        };
                      });
                    }}
                  />
                  <button
                    type="button"
                    className="mr-2 h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                    onClick={() => {
                      if (sittingOut) return;
                      console.log("executing");
                      setRoundEntry((prev) => {
                        if (prev == 0) return prev;
                        const newEntry = { ...prev, Loners: prev.Loners - 1 };
                        updateLoners(newEntry);
                        return newEntry;
                      });
                    }}
                  >
                    -
                  </button>
                  <button
                    type="button"
                    className="h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                    onClick={() => {
                      if (sittingOut) return;
                      console.log("executing");
                      setRoundEntry((prev) => {
                        const newEntry = { ...prev, Loners: prev.Loners + 1 };
                        updateLoners(newEntry);
                        return newEntry;
                      });
                    }}
                  >
                    +
                  </button>
                </div>
              </div>
              <div className="flex w-full justify-center">
                <input
                  value="Submit Score"
                  type="submit"
                  disabled={entrySubmitted}
                  className={`mx-auto mt-6 rounded-md border-2 border-slate-600 p-2 hover:cursor-pointer ${!entrySubmitted ? "bg-white" : "bg-slate-300 text-slate-400"}`}
                />
              </div>
            </form>
          </div>
        </>
      )}
      <div className="mt-6 flex justify-around">
        <div className="text-center">
          <p className="text-3xl font-bold">{Schedule.length - CurrentRound}</p>
          <p className="text-xs">Rounds Left</p>
        </div>
        <div className="text-center">
          <p className="text-3xl font-bold">2</p>
          <p className="text-xs">Rounds til break</p>
        </div>
      </div>
      <div className="mt-6 px-4">
        <div className="flex h-8 justify-between align-middle">
          <p className="mt-2 text-xs font-bold">Highest Round Score</p>
          <p className="text-lg tabular-nums">
            <span className="mr-4 text-xs italic">[Awarded Person]</span>
            10
          </p>
        </div>
        <div className="flex h-8 justify-between align-middle">
          <p className="mt-2 text-xs font-bold">Your Highest Round Score</p>
          <p className="text-lg tabular-nums">
            <span className="mr-4 text-xs italic">[Awarded Person]</span>
            10
          </p>
        </div>
        <div className="flex h-8 justify-between align-middle">
          <p className="mt-2 text-xs font-bold">Most Loners In One Round</p>
          <p className="text-lg tabular-nums">
            <span className="mr-4 text-xs italic">[Awarded Person]</span>
            10
          </p>
        </div>
      </div>
    </div>
  );
}

export default Tournament;
