import React, { useEffect, useState } from "react";

import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

import api from "../api";

function Home() {
  const { user } = useAuth();

  const [points, setPoints] = useState("");

  useEffect(() => {
    api
      .playerData("Histopolio", "Turma1", user.id)
      .then((res) => {
        setPoints(res.data.points);
      })
      .catch((error) => {
        console.log(error.message);
      });
  });

  return (
    <div className="text-center">
      <h1>Olá {user.name}</h1>
      <Link to="/play" style={{ textDecoration: "none" }}>
        <button className="btn btn-primary btn-lg mt-4">
          Jogar Histopólio
        </button>
      </Link>
      <h3 className="mt-3">Os teus pontos: {points}</h3>
    </div>
  );
}

export default Home;
