import React from "react";
import { BrowserRouter } from "react-router-dom";

import { useAuth } from "../providers/AuthProvider";
import AuthNavigator from "./AuthNavigator";
import AppNavigator from "./AppNavigator";

function RootNavigator() {
  const { user } = useAuth();

  return (
    <BrowserRouter>{user ? <AppNavigator /> : <AuthNavigator />}</BrowserRouter>
  );
}

export default RootNavigator;
