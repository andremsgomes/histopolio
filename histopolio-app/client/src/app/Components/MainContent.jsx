import React, { Component } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Menu from "../pages/Menu";

import Play from "./../pages/Play";

class MainContent extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <BrowserRouter>
        <Routes>
          <Route path="/kfsnfskdn" element={<Menu />} />
          <Route path="jnfsjn" element={<Play />} />
        </Routes>
      </BrowserRouter>
    );
  }
}

export default MainContent;
