import React, { useState } from "react";
import { Link } from "react-router-dom";

import api from "../api";

function Signup() {
  const [name, setName] = useState("");
  const [nameErrorMessage, setNameErrorMessage] = useState("");
  const [avatar, setAvatar] = useState("");
  const [email, setEmail] = useState("");
  const [emailErrorMessage, setEmailErrorMessage] = useState("");
  const [password, setPassword] = useState("");
  const [passwordErrorMessage, setPasswordErrorMessage] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [
    confirmPasswordErrorMessage,
    setConfirmPasswordErrorMessage,
  ] = useState("");
  const [alertMessage, setAlertMessage] = useState("");

  const handleClick = () => {
    let nameError = false;
    let emailError = false;
    let passwordError = false;
    let confirmPasswordError = false;

    setNameErrorMessage("");
    setEmailErrorMessage("");
    setPasswordErrorMessage("");
    setConfirmPasswordErrorMessage("");

    const nameInput = document.getElementById("nameInput");
    nameInput.style.borderColor = "#ced4da";

    const emailInput = document.getElementById("emailInput");
    emailInput.style.borderColor = "#ced4da";

    const passwordInput = document.getElementById("passwordInput");
    passwordInput.style.borderColor = "#ced4da";

    const confirmPasswordInput = document.getElementById(
      "confirmPasswordInput"
    );
    confirmPasswordInput.style.borderColor = "#ced4da";

    // name validation
    if (name.length === 0) {
      nameError = true;
      setNameErrorMessage("Por favor introduz o teu nome.");
    }
    // TODO: verificar tamanho máximo

    if (nameError) {
      nameInput.style.borderColor = "red";
    }

    // avatar validation
    const avatarToSend =
      avatar.length > 0 ? avatar : "https://www.linkpicture.com/q/user_21.png";

    // email validation
    if (email.length === 0) {
      emailError = true;
      setEmailErrorMessage("Por favor introduz um email válido.");
    } else if (!/\w*@up.pt/.test(email)) {
      emailError = true;
      setEmailErrorMessage(
        "Por favor introduz um email válido (formato: utilizador@up.pt)."
      );
    }

    if (emailError) {
      emailInput.style.borderColor = "red";
    }

    // password validation
    if (password.length === 0) {
      passwordError = true;
      setPasswordErrorMessage("Por favor introduz uma password.");
    }

    if (passwordError) {
      passwordInput.style.borderColor = "red";
    }

    // confirm password validation
    if (confirmPassword.length === 0) {
      confirmPasswordError = true;
      setConfirmPasswordErrorMessage("Confirma a tua password.");
    } else if (password !== confirmPassword) {
      confirmPasswordError = true;
      setConfirmPasswordErrorMessage("As passwords não são iguais.");
    }

    if (confirmPasswordError) {
      confirmPasswordInput.style.borderColor = "red";
    }

    if (!(nameError || emailError || passwordError || confirmPasswordError)) {
      const payload = { name, avatarToSend, email, password };

      api
        .signup(payload)
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

  const handleNameChange = (e) => {
    setName(e.target.value);
  };

  const handleAvatarChange = (e) => {
    setAvatar(e.target.value);
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
    <div className="row m-4">
      <div className="col-sm-12 col-md-8 col-lg-6 mx-auto">
        {alertMessage.length > 0 && (
          <div className="alert alert-danger" role="alert">
            {alertMessage}
          </div>
        )}
        <div className="form-group row">
          <label for="name" className="col-sm-3 col-form-label">
            Nome
          </label>
          <div className="col-sm-9">
            <input
              type="text"
              className="form-control"
              id="nameInput"
              name="name"
              onChange={handleNameChange}
              value={name}
            />
            <div className="text-danger">{nameErrorMessage}</div>
          </div>
        </div>
        <div className="form-group row mt-3">
          <label for="avatar" className="col-sm-3 col-form-label">
            Link do Avatar
          </label>
          <div className="col-sm-9">
            <input
              type="text"
              className="form-control"
              id="avatarInput"
              name="avatar"
              onChange={handleAvatarChange}
              value={avatar}
            />
          </div>
        </div>
        <div className="text-center mt-3">
          {avatar.length > 0 ? (
            <img
              src={avatar}
              alt="Avatar"
              className="rounded-circle border"
              style={{ objectFit: "cover", width: "250px", height: "250px" }}
            />
          ) : (
            <img
              src="https://www.linkpicture.com/q/user_21.png"
              alt="Avatar"
              className="rounded-circle border"
              style={{ objectFit: "cover", width: "250px", height: "250px" }}
            />
          )}
        </div>
        <div className="form-group row mt-3">
          <label for="email" className="col-sm-3 col-form-label">
            Email
          </label>
          <div className="col-sm-9">
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
          <label for="password" className="col-sm-3 col-form-label">
            Password
          </label>
          <div className="col-sm-9">
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
        <div className="form-group row mt-3">
          <label for="confirmPassword" className="col-sm-3 col-form-label">
            Confirma a Password
          </label>
          <div className="col-sm-9">
            <input
              type="password"
              className="form-control"
              id="confirmPasswordInput"
              name="confirmPassword"
              onChange={handleConfirmPasswordChange}
              value={confirmPassword}
            />
            <div className="text-danger">{confirmPasswordErrorMessage}</div>
          </div>
        </div>
        <div className="text-center">
          <button className="btn btn-primary btn-lg mt-4" onClick={handleClick}>
            Criar conta
          </button>
        </div>
        <p className="mt-4 text-center">
          Já tens uma conta? Faz o login <Link to="/login">aqui</Link>.
        </p>
      </div>
    </div>
  );
}

export default Signup;
