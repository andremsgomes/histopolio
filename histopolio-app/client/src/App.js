import "./App.css";
import React from "react";

import { AuthProvider } from "./app/providers/AuthProvider";
import RootNavigator from "./app/navigation/RootNavigator";

function App() {
  return (
    <AuthProvider>
      <RootNavigator />
    </AuthProvider>
  );
}

export default App;
