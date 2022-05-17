import React from "react";
import { BrowserRouter } from "react-router-dom";

import AuthNavigator from "./AuthNavigator";
import AppNavigator from "./AppNavigator";
import AdminNavigator from "./AdminNavigator";

function RootNavigator() {
  const user = JSON.parse(sessionStorage.getItem("user"));

  return (
    <BrowserRouter>{user ? (user.admin ? <AdminNavigator /> : <AppNavigator />) : <AuthNavigator />}</BrowserRouter>
  );
}

export default RootNavigator;
