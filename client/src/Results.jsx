import React, { useState } from "react";

// On the backend, generate a results class that has all of the needed information and send it here.

function Results({ room, results }) {
  const [isHost, setIsHost] = useState(true);
  const [resultsShared, setResultsShared] = useState(false);

  const handleShareResults = () => {
    const confirmed = confirm(
      "Clicking OK will release the results to all players.",
    );
    if (confirmed) {
      setResultsShared(true);
    }
  };

  if (!isHost && !resultsShared) {
    return (
      <div className="mt-10 h-40 w-72 text-center">
        <p className="text-2xl">Thank you for playing!</p>
        <p className="text">Please wait for results to be released.</p>
        <p className="mx-auto mt-6 text-xs">
          Tournament results will be shared by the host shortly.
        </p>
        <p className="mx-auto mt-2 text-xs">
          After the results are shared, you will be able to view them here.
        </p>
      </div>
    );
  } else {
    return (
      <div className="w-full max-w-full overflow-x-scroll px-2">
        <h1 className="mt-2 text-center text-3xl font-bold">Results</h1>
        <div>
          <h2 className="ml-2 mt-4 text-xl font-semibold">Leaderboard</h2>
          <table className="mt-2 w-full overflow-x-scroll text-center">
            <thead>
              <tr key="heading">
                <th>#</th>
                <th>Player</th>
                <th>Score</th>
                <th>Loners</th>
              </tr>
            </thead>
            <tbody>
              {results.leaderboard.map((player, index) => {
                return (
                  <tr key={player.uuid}>
                    <td>{index + 1}</td>
                    <td>{player.username}</td>
                    <td>{player.totalScore}</td>
                    <td>{player.totalLoners}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
        <div className="mt-8 w-full">
          <h2 className="ml-2 mt-4 text-xl font-semibold">
            Honorable Mentions
          </h2>
          <table className="ml-2 mt-2 w-[95%]">
            <tbody>
              {Object.keys(results.honorableMentions).map((key) => {
                const award = results.honorableMentions[key];
                return (
                  <tr key={key}>
                    <td className="text-xs">{award.title}</td>
                    <td className="font-bold">{award.player.username}: </td>
                    <td>{award.value}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
        <div>
          <h2 className="ml-2 mt-4 text-xl font-semibold">
            Dishonorable Mentions
          </h2>
          <table className="ml-2 mt-2 w-[95%]">
            <tbody>
              {Object.keys(results.dishonorableMentions).map((key) => {
                const award = results.dishonorableMentions[key];
                return (
                  <tr key={key}>
                    <td className="text-xs">{award.title}</td>
                    <td className="font-bold">{award.player.username}: </td>
                    <td>{award.value}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
        {isHost && !resultsShared ? (
          <div className="mt-6 flex w-full justify-center">
            <button
              type="button"
              onClick={handleShareResults}
              className="mx-auto h-10 w-[80%] rounded-md border-2 border-slate-700 bg-white text-lg"
            >
              Share Results
            </button>
          </div>
        ) : (
          <></>
        )}
      </div>
    );
  }
}

export default Results;
