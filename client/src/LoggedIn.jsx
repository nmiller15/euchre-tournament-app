import React, { useState, useEffect } from "react";
import useWebSocket from "react-use-websocket";
// import { createContext } from "react";
import Lobby from "./Lobby";
import Results from "./Results";
import Tournament from "./Tournament";
import { handleMessage } from "./components/handleMessage";

const WS_URL = "ws://127.0.0.1:8080";

// Create a "message handler" that will update state for all things!

function LoggedIn({ form }) {
  const [room, setRoom] = useState({});
  const [user, setUser] = useState({});
  // const [team, setTeam] = useState({});
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
      setMessageHistory((prev) => [jsonMessage, ...prev]);
      handleMessage(jsonMessage, room, setRoom, user, setUser);
    }
  };

  useEffect(() => {
    // Check the message list for Room:Create and User:Create messages
    if (Object.keys(room).length == 0) {
      messageHistory.forEach((message) => {
        if (message.Type == "Room:Create" || message.Type == "Room:Join") {
          setRoom(message.RoomPayload);
        }
      });
    }
    if (Object.keys(user).length == 0) {
      let found = false;
      messageHistory.forEach((message) => {
        if (message.Type == "User:Create") {
          setUser(message.UserPayload);
          found = true;
        }
      });
      // If the user isn't found, request the user object response from the server.
      if (!found) {
        const message = {
          Type: "User",
        };
        sendMessage(JSON.stringify(message));
      }
    }
    // Call the message handler for state updates for all other messages:
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
            {room && room.CurrentRound == 0 && (
              <Lobby room={room} user={user} send={sendMessage} />
            )}
            {/* <Tournament room={roomModel} /> */}
            {/* <Results room={roomModel} results={results} /> */}
          </div>
        </div>
      </>
    )
  );
}

export default LoggedIn;
