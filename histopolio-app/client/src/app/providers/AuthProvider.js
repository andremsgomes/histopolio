import React, { useState, createContext, useContext } from "react";

import api from "../api";

const AuthContext = createContext();

function AuthProvider({ children }) {
  const [user, setUser] = useState(null);

  async function login(email, password) {
    const payload = { email, password };

    await api
      .login(payload)
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
        login,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

const useAuth = () => useContext(AuthContext);

export { AuthProvider, useAuth };
