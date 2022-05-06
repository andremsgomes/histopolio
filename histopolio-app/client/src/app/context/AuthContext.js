import React, { useState, createContext, useContext } from "react";

import api from "../api";

const AuthContext = createContext();

function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [errorMessage, setErrorMessage] = useState("");

  async function login(email, password) {
    const payload = { email, password };

    await api
      .login(payload)
      .then((res) => {
        setErrorMessage("");
        setUser(res.data);
      })
      .catch((error) => {
        setErrorMessage(error.response.data.message);
      });
  }

  async function signup(name, email, password, passwordConfirmation) {
    if (password !== passwordConfirmation) {
      window.alert("Passwords não são iguais");
      return;
    }

    const payload = { name, email, password };

    await api
      .signup(payload)
      .then((res) => {
        setUser(res.data);
      })
      .catch((error) => {
        window.alert(error.message);
      });
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        errorMessage,
        login,
        signup,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

const useAuth = () => useContext(AuthContext);

export default AuthContext;

export { AuthProvider, useAuth };
