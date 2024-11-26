import { useState, useEffect } from "react";
import { tableContainsPlayer } from "./util/tableContainsPlayer";
import { getPartner } from "./util/getPartner";

function Tournament({ room, user, send }) {
  // All tournament data is from the room object
  const { Schedule, CurrentRound } = room;
  // State of the data on the screen is determined by which round is in state.
  const [displayRound, setDisplayRound] = useState(
    Schedule.find((round) => round.RoundNumber == CurrentRound),
  );
  const [currentScore, setCurrentScore] = useState(0);
  const [currentLoners, setCurrentLoners] = useState(0);
  const [currentPartner, setCurrentPartner] = useState(
    getPartner(displayRound, user.Guid),
  );

  // Round changer interface logic
  const handleSeePrevRound = () => {
    const roundNumber = displayRound.RoundNumber;
    if (roundNumber == 1) return;
    const prevRoundObj = Schedule.find(
      (round) => round.RoundNumber == roundNumber - 1,
    );
    setDisplayRound(prevRoundObj);
  };

  const handleSeeNextRound = () => {
    const roundNumber = displayRound.RoundNumber;
    if (roundNumber == Schedule.length) return;
    const nextRoundObj = Schedule.find(
      (round) => round.RoundNumber == roundNumber + 1,
    );
    setDisplayRound(nextRoundObj);
  };
  //##########################################

  const handleSubmit = (e) => {
    e.preventDefault();
    const result = confirm(
      `Click "OK" to submit your score of ${currentScore} point(s) ${currentLoners} loner(s).`,
    );
    if (!result) return;
    console.log("Submitted");
  };

  useEffect(() => {
    if (displayRound) {
      setCurrentPartner(getPartner(displayRound, user.Guid));
    }
  }, [displayRound]);

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
      <div className="mt-6 flex justify-center gap-4">
        {displayRound.RoundTables.map((table) => {
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
        })}
        {/* ######################## */}
      </div>
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
                value={currentScore}
                className="mr-8 h-10 w-10 rounded-md border-2 border-slate-600 text-center text-lg font-semibold"
                onChange={(e) => setCurrentScore(e.target.value)}
              />
              <button
                type="button"
                className="mr-2 h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                onClick={() => {
                  setCurrentScore((prev) => {
                    if (prev == 0) return prev;
                    return prev - 1;
                  });
                }}
              >
                -
              </button>
              <button
                type="button"
                className="h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                onClick={() => {
                  setCurrentScore((prev) => prev + 1);
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
                value={currentLoners}
                className="mr-8 h-10 w-10 rounded-md border-2 border-slate-600 text-center text-lg font-semibold"
                onChange={(e) => setCurrentLoners(e.target.value)}
              />
              <button
                type="button"
                className="mr-2 h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                onClick={() => {
                  setCurrentLoners((prev) => {
                    if (prev == 0) return prev;
                    return prev - 1;
                  });
                }}
              >
                -
              </button>
              <button
                type="button"
                className="h-8 w-8 border-2 border-slate-600 bg-white text-xl font-bold"
                onClick={() => {
                  setCurrentLoners((prev) => prev + 1);
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
              className="mx-auto mt-6 rounded-md border-2 border-slate-600 bg-white p-2"
            />
          </div>
        </form>
      </div>
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
