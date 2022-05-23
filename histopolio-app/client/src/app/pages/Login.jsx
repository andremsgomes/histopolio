import React, { useState } from "react";
import { Link } from "react-router-dom";

import api from "../api";

function Login() {
  const [email, setEmail] = useState("");
  const [emailErrorMessage, setEmailErrorMessage] = useState("");
  const [password, setPassword] = useState("");
  const [passwordErrorMessage, setPasswordErrorMessage] = useState("");
  const [alertMessage, setAlertMessage] = useState("");

  const handleClick = () => {
    let emailError = false;
    let passwordError = false;

    setEmailErrorMessage("");
    setPasswordErrorMessage("");

    const emailInput = document.getElementById("emailInput");
    emailInput.style.borderColor = "#ced4da";

    const passwordInput = document.getElementById("passwordInput");
    passwordInput.style.borderColor = "#ced4da";

    // email validation
    if (email.length === 0) {
      emailError = true;
      setEmailErrorMessage("Por favor introduz um email válido.");
    } else if (!(/\w*@up.pt/.test(email) || /\w*@edu.\w.up.pt/.test(email))) {
      emailError = true;
      setEmailErrorMessage(
        "Por favor introduz um email válido (formato: utilizador@up.pt ou utilizador@edu.instituição.up.pt)."
      );
    }

    if (emailError) {
      emailInput.style.borderColor = "red";
    }

    // password validation
    if (password.length === 0) {
      passwordError = true;
      setPasswordErrorMessage("Por favor introduz a tua password.");
    }

    if (passwordError) {
      passwordInput.style.borderColor = "red";
    }

    if (!(emailError || passwordError)) {
      const payload = { email, password };

      api
        .login(payload)
        .then((res) => {
          sessionStorage.setItem("user", JSON.stringify(res.data));
          window.location.href = "/";
        })
        .catch((error) => {
          if (error.response?.data?.message)
            setAlertMessage(error.response.data.message);
          else setAlertMessage(error.message);
        });
    }
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
        {alertMessage.length > 0 && (
          <div className="alert alert-danger" role="alert">
            {alertMessage}
          </div>
        )}
        <div className="form-group row">
          <label for="email" className="col-sm-2 col-form-label">
            Email
          </label>
          <div className="col-sm-10">
            <input
              type="email"
              className="form-control"
              id="emailInput"
              name="email"
              onChange={handleEmailChange}
              value={email}
            />
            <div className="text-danger">{emailErrorMessage}</div>
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
              id="passwordInput"
              name="password"
              onChange={handlePasswordChange}
              value={password}
            />
            <div className="text-danger">{passwordErrorMessage}</div>
          </div>
        </div>
        <div className="text-center">
          <button className="btn btn-primary btn-lg mt-4" onClick={handleClick}>
            Login
          </button>
        </div>
        <p className="mt-4 text-center">
          Não tens uma conta? Regista-te <Link to="/signup">aqui</Link>.
        </p>
      </div>
    </div>
  );
}

export default Login;
