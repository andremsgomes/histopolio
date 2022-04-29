import React from "react";

import { Link } from "react-router-dom";

function Menu() {
  return (
    <div>
      <Link to="/play" style={{ textDecoration: "none" }}>
        <button className="btn btn-primary btn-lg">Jogar</button>
      </Link>
    </div>
  );
}

export default Menu;
