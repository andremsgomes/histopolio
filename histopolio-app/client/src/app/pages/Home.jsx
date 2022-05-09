import React from "react";

import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

function Home() {
  const { user } = useAuth();

  return (
    <div className="text-center">
      <h1>Olá {user.name}</h1>
      <Link to="/play" style={{ textDecoration: "none" }}>
        <button className="btn btn-primary btn-lg mt-4">
          Jogar Histopólio
        </button>
      </Link>
      <h3 className="mt-3">Os teus pontos: {user.game.points}</h3>
    </div>
  );
}

export default Home;
