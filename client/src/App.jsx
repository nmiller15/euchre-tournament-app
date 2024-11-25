import { useState } from "react";
import { roomModel } from "../mock/roomModel";
import { results } from "../mock/results";
import Login from "./components/Login";
import LoggedIn from "./LoggedIn";

function App() {
  const [form, setForm] = useState({
    username: "",
    newRoom: false,
    roomCode: "",
    submitted: false,
  });

  return !form.submitted ? (
    <div className="flex min-h-screen justify-center">
      <div className="flex w-[360px] justify-center bg-slate-100">
        <Login form={form} setForm={setForm} />
      </div>
    </div>
  ) : (
    <div className="flex min-h-screen justify-center">
      <div className="flex w-[360px] justify-center bg-slate-100">
        <LoggedIn form={form} />
      </div>
    </div>
  );
}

export default App;
