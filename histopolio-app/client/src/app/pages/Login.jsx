import React, { useState } from "react";

import { useAuth } from "../providers/AuthProvider";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const { login } = useAuth();

  const handleClick = () => {
    login(email, password);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  return (
    <div>
      <input
        type="text"
        name="email"
        onChange={handleEmailChange}
        placeholder="Email"
        value={email}
      />
      <input
        type="password"
        name="password"
        onChange={handlePasswordChange}
        placeholder="Password"
        value={password}
      />
      <button onClick={handleClick}>Login</button>
    </div>
  );
}

export default Login;
