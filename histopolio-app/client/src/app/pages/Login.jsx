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
    <div className="row m-4">
      <div className="col-sm-12 col-md-8 col-lg-6 mx-auto">
        <form>
          <div className="form-group row">
            <label for="email" className="col-sm-2 col-form-label">
              Email
            </label>
            <div className="col-sm-10">
              <input
                type="email"
                className="form-control"
                id="email"
                name="email"
                onChange={handleEmailChange}
                value={email}
              />
            </div>
          </div>
          <div className="form-group row mt-3">
            <label for="password" className="col-sm-2 col-form-label">
              Password
            </label>
            <div className="col-sm-10">
              <input
                type="password"
                className="form-control"
                id="password"
                name="password"
                onChange={handlePasswordChange}
                value={password}
              />
            </div>
          </div>
          <div className="text-center">
            <button
              className="btn btn-primary btn-lg mt-4"
              onClick={handleClick}
            >
              Login
            </button>
          </div>
        </form>
        <p className="mt-4 text-center">
          Não tens uma conta? Regista-te <Link to="/signup">aqui</Link>
        </p>
      </div>
    </div>
  );
}

export default Login;
