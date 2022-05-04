import React, { useState } from "react";
import { Link } from "react-router-dom";

import { useAuth } from "../context/AuthContext";

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
        type="email"
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
      <p>Não tens conta? Cria uma <Link to="/signup">aqui</Link></p>
    </div>
  );
}

export default Login;
