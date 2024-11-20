import { useState } from "react";
import Login from "./components/Login";
import Lobby from "./Lobby";
import Results from "./Results";
import Tournament from "./Tournament";
import { roomModel } from "../mock/roomModel";
import { results } from "../mock/results";

function App() {
  return (
    <div className="flex min-h-screen justify-center">
      <div className="flex w-[360px] justify-center bg-slate-100">
        {/* <Lobby room={roomModel} /> */}
        {/* <Login /> */}
        {/* <Tournament room={roomModel} /> */}
        <Results room={roomModel} results={results} />
      </div>
    </div>
  );
}

export default App;
