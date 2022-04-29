import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Menu from "../pages/Menu";
import Play from "../pages/Play";

function AppNavigator() {
  return (
    <Routes>
      <Route path="/" element={<Menu />} />
      <Route path="/play" element={<Play />} />
      <Route path="/login" element={<Navigate replace to="/" />} />
    </Routes>
  );
}

export default AppNavigator;
