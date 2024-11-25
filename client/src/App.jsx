import { useEffect, useState } from "react";
import Login from "./components/Login";
import Lobby from "./Lobby";
import Results from "./Results";
import Tournament from "./Tournament";
import { roomModel } from "../mock/roomModel";
import { results } from "../mock/results";
import useWebSocket from "react-use-websocket";

function App() {
  const WS_URL = "ws://127.0.0.1:8080";
  const { sendJsonMessage, lastJsonMessage } = useWebSocket(WS_URL, {
    share: true,
    queryParams: {
      username: "user1",
      createRoom: "true",
    },
  });

  useEffect(() => {
    sendJsonMessage({
      message: "Hello",
    });
  }, []);

  useEffect(() => {
    console.log(lastJsonMessage);
  }, [lastJsonMessage]);

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
