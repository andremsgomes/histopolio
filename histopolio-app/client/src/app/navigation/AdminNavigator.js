import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Admin from "../pages/Admin";
import EditSave from "../pages/EditSave";
import EditBoard from "../pages/EditBoard";

function AdminNavigator() {
  return (
    <Routes>
      <Route path="/admin" element={<Admin />} />
      <Route path="/admin/edit/:board" element={<EditBoard />} />
      <Route path="/admin/edit/:board/:save" element={<EditSave />} />
      <Route path="/*" element={<Navigate replace to="/admin" />} />
    </Routes>
  );
}

export default AdminNavigator;
