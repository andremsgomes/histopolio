import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Admin from "../pages/Admin";
import Save from "../pages/Save";

function AdminNavigator() {
  return (
    <Routes>
      <Route path="/admin" element={<Admin />} />
      <Route path="/admin/:board/:save" element={<Save />} />
      <Route path="/*" element={<Navigate replace to="/admin" />} />
    </Routes>
  );
}

export default AdminNavigator;
