import React from "react";

function Wait(props) {
  return (
    <div className="text-center">
      <h2>Espera pelo início do jogo!</h2>
      <h4 className="mt-4">Os teus pontos: {props.points}</h4>
    </div>
  );
}

export default Wait;