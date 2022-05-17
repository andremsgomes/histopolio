import React from "react";

import { Route, Routes, Navigate } from "react-router-dom";

import Admin from "../pages/Admin";
import EditSave from "../pages/EditSave";
import EditBoard from "../pages/EditBoard";
import EditQuestions from "../pages/EditQuestions";
import NewQuestion from "../pages/NewQuestion";
import NewCommunityCard from "../pages/NewCommunityCard";

function AdminNavigator() {
  return (
    <Routes>
      <Route path="/admin" element={<Admin />} />
      <Route path="/admin/:board" element={<EditBoard />} />
      <Route path="/admin/:board/:tile/questions" element={<EditQuestions />} />
      <Route path="/admin/:board/:tile/questions/new" element={<NewQuestion />} />
      <Route path="/admin/:board/cards/community_cards/new" element={<NewCommunityCard />} />
      <Route path="/admin/:board/:save" element={<EditSave />} />
      <Route path="/*" element={<Navigate replace to="/admin" />} />
    </Routes>
  );
}

export default AdminNavigator;
