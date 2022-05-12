import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Admin from "../pages/Admin";
import EditSave from "../pages/EditSave";
import EditBoard from "../pages/EditBoard";
import EditQuestions from "../pages/EditQuestions";

function AdminNavigator() {
  return (
    <Routes>
      <Route path="/admin" element={<Admin />} />
      <Route path="/admin/edit/:board" element={<EditBoard />} />
      <Route path="/admin/edit/:board/:tile/questions" element={<EditQuestions />} />
      <Route path="/admin/edit/:board/:save" element={<EditSave />} />
      <Route path="/*" element={<Navigate replace to="/admin" />} />
    </Routes>
  );
}

export default AdminNavigator;
