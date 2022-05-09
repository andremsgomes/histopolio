import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Home from "../pages/Home";

function AdminNavigator() {
  return (
    <Routes>
      <Route path="/admin" element={<Home />} />
      <Route path="/*" element={<Navigate replace to="/admin" />} />
    </Routes>
  );
}

export default AdminNavigator;
