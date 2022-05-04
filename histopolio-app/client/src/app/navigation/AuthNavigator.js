import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Login from "../pages/Login";
import Signup from "../pages/Signup";

function AuthNavigator() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/signup" element={<Signup />} />
      <Route path="/*" element={<Navigate replace to="/login" />} />
    </Routes>
  );
}

export default AuthNavigator;
