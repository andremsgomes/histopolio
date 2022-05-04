import React, { useState } from "react";
import { Link } from "react-router-dom";

import { useAuth } from "../context/AuthContext";

function Signup() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const { signup } = useAuth()

  const handleClick = () => {
    signup(name, email, password, confirmPassword);
  };

  const handleNameChange = (e) => {
    setName(e.target.value);
  };

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleConfirmPasswordChange = (e) => {
    setConfirmPassword(e.target.value);
  };

  return (
    <div>
      <input
        type="text"
        name="name"
        onChange={handleNameChange}
        placeholder="Nome"
        value={name}
      />
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
      <input
        type="password"
        name="confirmPassword"
        onChange={handleConfirmPasswordChange}
        placeholder="Confirma a Password"
        value={confirmPassword}
      />
      <button onClick={handleClick}>Criar conta</button>
      <p>Já tens uma conta? Faz o login <Link to="/login">aqui</Link></p>
    </div>
  );
}

export default Signup;
