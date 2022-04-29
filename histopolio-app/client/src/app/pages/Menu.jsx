import React from "react";

import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

function Menu() {
  const { user } = useAuth();

  return (
    <div>
      <h1>Olá {user.name}</h1>
      <Link to="/play" style={{ textDecoration: "none" }}>
        <button className="btn btn-primary btn-lg">Jogar</button>
      </Link>
    </div>
  );
}

export default Menu;
