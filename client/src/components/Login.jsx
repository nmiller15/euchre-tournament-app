import { useState } from "react";

function Login() {
  const [form, setForm] = useState({
    username: "",
    newRoom: false,
    roomCode: "",
  });

  const [error, setError] = useState({});

  const errorFieldStyleString = "border-red-400 border-solid border-2";

  const handleLoginFormErrors = () => {
    setError({});
    if (!form.username) {
      setError({
        id: "username",
        error: "No Username",
        message: "You must enter a username.",
      });
      return true;
    }
    if (!form.roomCode && !form.newRoom) {
      setError({
        id: "roomCode",
        error: "No Room Code",
        message: "Enter a room code.",
      });
      return true;
    }
    if (!form.newRoom && form.roomCode.length != 4) {
      setError({
        id: "roomCode",
        error: "Invalid length.",
        message: "Room codes are 4 digits.",
      });
      return true;
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const errorsPresent = handleLoginFormErrors();
    if (errorsPresent) return;
  };
  return (
    <>
      <form onSubmit={handleSubmit}>
        <h1 className="text-4xl mt-4">Euchre Tournament</h1>
        {error?.id == "username" && (
          <div className="flex justify-center -mb-4 mt-4">
            <p className="text-red-400 text-sm text-center">{error.message}</p>
          </div>
        )}
        <input
          className={`block w-full text-center mt-6 h-8 font-semibold rounded-md ${
            error?.id == "username" && errorFieldStyleString
          }`}
          type="text"
          id="username"
          value={form.username}
          placeholder="Enter Name"
          onChange={(e) =>
            setForm((prev) => {
              return { ...prev, username: e.target.value };
            })
          }
        />
        <div className="flex justify-center mt-4">
          <input
            type="checkbox"
            id="newRoom"
            checked={form.newRoom}
            onChange={(e) => {
              setForm((prev) => {
                return { ...prev, newRoom: e.target.checked };
              });
            }}
          />
          <label htmlFor="newRoom" className="ml-4">
            Host a new room
          </label>
        </div>
        {form.newRoom ? (
          <></>
        ) : (
          <>
            {error?.id == "roomCode" && (
              <div className="flex justify-center -mb-4 mt-4">
                <p className="text-red-400 text-xs text-center">
                  {error.message}
                </p>
              </div>
            )}
            <div className="flex justify-center mt-4 align-center">
              <label htmlFor="roomCode">Enter a Room Code:</label>
              <input
                type="text"
                className={`w-16 text-center ml-4 rounded-md h-8 font-bold ${
                  error?.id == "roomCode" && errorFieldStyleString
                }`}
                id="roomCode"
                value={form.roomCode}
                placeholder="XXXX"
                maxLength="4"
                onChange={(e) => {
                  setForm((prev) => {
                    return {
                      ...prev,
                      roomCode: e.target.value.toUpperCase(),
                    };
                  });
                }}
              />
            </div>
          </>
        )}
        <div className="flex justify-center">
          <input
            className="mt-6 text-3xl font-semibold text-center py-2 px-4 bg-slate-500 rounded-md "
            type="submit"
            value={form.newRoom ? "Host Game" : "Join Game"}
          />
        </div>
      </form>
    </>
  );
}

export default Login;
