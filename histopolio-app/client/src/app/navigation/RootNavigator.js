import React from "react";
import { BrowserRouter } from "react-router-dom";

import { useAuth } from "../context/AuthContext";
import AuthNavigator from "./AuthNavigator";
import AppNavigator from "./AppNavigator";
import AdminNavigator from "./AdminNavigator";

function RootNavigator() {
  const { user } = useAuth();

  return (
    <BrowserRouter>{user ? (user.admin ? <AdminNavigator /> : <AppNavigator />) : <AuthNavigator />}</BrowserRouter>
  );
}

export default RootNavigator;
