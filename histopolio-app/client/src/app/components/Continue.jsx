import React from "react";

function Content(props) {
  return (
    <div className="text-center page-center">
      {props.info.length > 0 && <h2 className="mx-4">{props.info}</h2>}
      <button
        className="btn btn-primary btn-lg continue"
        onClick={props.onContinueClick}
      >
        Continuar
      </button>
      <div>
        {props.rank !== 0 && <h4>Estás em {props.rank}º lugar</h4>}
        <h5>
          Tens {props.points} ponto{props.points !== 1 && "s"}
        </h5>
      </div>
      {props.storeButton && (
        <button
          className="btn btn-lg btn-primary mt-4"
          onClick={props.onStoreClick}
        >
          Comprar troféus
        </button>
      )}
    </div>
  );
}

export default Content;
