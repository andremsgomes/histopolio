import React from "react";

import { Link } from "react-router-dom";

function Wait(props) {
  return (
    <div className="m-4">
      <nav aria-label="breadcrumb" className="m-4">
        <ol className="breadcrumb">
          <li className="breadcrumb-item">
            <Link to="/">Home</Link>
          </li>
          <li className="breadcrumb-item active" aria-current="page">
            Histopolio
          </li>
        </ol>
      </nav>
      <div className="text-center page-center">
        <h2>Espera pelo início do jogo!</h2>
        <div className="mt-4">
          {props.rank !== 0 && <h4>Estás em {props.rank}º lugar</h4>}
          <h5>
            Tens {props.points} ponto{props.points !== 1 && "s"}
          </h5>
        </div>
      </div>
    </div>
  );
}

export default Wait;
