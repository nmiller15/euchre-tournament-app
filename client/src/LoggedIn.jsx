import React, { useState, useEffect } from "react";
import useWebSocket from "react-use-websocket";
// import { createContext } from "react";
import Lobby from "./Lobby";
import Results from "./Results";
import Tournament from "./Tournament";

const WS_URL = "ws://127.0.0.1:8080";

// Create a "message handler" that will update state for all things!

function LoggedIn({ form }) {
  const [room, setRoom] = useState({});
  const [user, setUser] = useState({});
  const [messageHistory, setMessageHistory] = useState([]);
  const { sendMessage, lastMessage, readyState } = useWebSocket(WS_URL, {
    share: true,
    queryParams: {
      username: form.username,
      createRoom: form.newRoom,
      roomCode: form.roomCode,
    },
  });

  const processMessage = async () => {
    if (lastMessage) {
      const jsonMessage = await JSON.parse(lastMessage.data);
      setMessageHistory((prev) => [...prev, jsonMessage]);
    }
  };

  useEffect(() => {
    console.log("attempt to set room and user");
    if (Object.keys(room).length == 0) {
      console.log("no room");
      messageHistory.forEach((message) => {
        if (message.Type == "Room:Create" || message.Type == "Room:Join") {
          setRoom(message.RoomPayload);
          console.log("room set");
        }
      });
    }
    if (Object.keys(user).length == 0) {
      console.log("no user");
      messageHistory.forEach((message) => {
        if (message.Type == "User:Create") {
          setUser(message.UserPayload);
          console.log("user set");
        }
      });
    }
  }, [messageHistory]);

  useEffect(() => {
    console.log("Message received.");

    processMessage();
  }, [lastMessage]);

  return (
    room &&
    user && (
      <>
        <div className="flex min-h-screen justify-center">
          <div className="flex w-[360px] justify-center bg-slate-100">
            {room && <Lobby room={room} user={user} />}
            {/* <Tournament room={roomModel} /> */}
            {/* <Results room={roomModel} results={results} /> */}
          </div>
        </div>
      </>
    )
  );
}

export default LoggedIn;
