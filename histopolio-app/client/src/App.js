import "./App.css";
import React from "react";

import { AuthProvider } from "./app/context/AuthContext";
import RootNavigator from "./app/navigation/RootNavigator";

function App() {
  return (
    <AuthProvider>
      <RootNavigator />
    </AuthProvider>
  );
}

export default App;
