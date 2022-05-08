import React, { useState, createContext, useContext } from "react";

import api from "../api";

const AuthContext = createContext();

function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [errorMessage, setErrorMessage] = useState("");

  async function login(email, password) {
    setErrorMessage("");

    const payload = { email, password };

    await api
      .login(payload)
      .then((res) => {
        setUser(res.data);
      })
      .catch((error) => {
        if (error.response?.data?.message)
          setErrorMessage(error.response.data.message);
        else setErrorMessage(error.message);
      });
  }

  async function signup(name, email, password) {
    setErrorMessage("");

    const payload = { name, email, password };

    await api
      .signup(payload)
      .then((res) => {
        setUser(res.data);
      })
      .catch((error) => {
        if (error.response?.data?.message)
          setErrorMessage(error.response.data.message);
        else setErrorMessage(error.message);
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
