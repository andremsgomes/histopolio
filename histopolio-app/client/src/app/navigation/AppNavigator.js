import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Home from "../pages/Home";
import Play from "../pages/Play";

function AppNavigator() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/play" element={<Play />} />
      <Route path="/*" element={<Navigate replace to="/" />} />
    </Routes>
  );
}

export default AppNavigator;
