import { useState } from "react";

function Login({ form, setForm }) {
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
    setForm((prev) => {
      return {
        ...prev,
        submitted: true,
      };
    });
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <h1 className="mt-4 text-4xl">Euchre Tournament</h1>
        {error?.id == "username" && (
          <div className="-mb-4 mt-4 flex justify-center">
            <p className="text-center text-sm text-red-400">{error.message}</p>
          </div>
        )}
        <input
          className={`mt-6 block h-8 w-full rounded-md text-center font-semibold ${
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
        <div className="mt-4 flex justify-center">
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
              <div className="-mb-4 mt-4 flex justify-center">
                <p className="text-center text-xs text-red-400">
                  {error.message}
                </p>
              </div>
            )}
            <div className="align-center mt-4 flex justify-center">
              <label htmlFor="roomCode">Enter a Room Code:</label>
              <input
                type="text"
                className={`ml-4 h-8 w-16 rounded-md text-center font-bold ${
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
            className="mt-6 rounded-md bg-slate-500 px-4 py-2 text-center text-3xl font-semibold"
            type="submit"
            value={form.newRoom ? "Host Game" : "Join Game"}
          />
        </div>
      </form>
    </>
  );
}

export default Login;
