import React from "react";

function Store(props) {
  return (
    <div className="text-center store">
      <h2 className="mt-4">Troféus</h2>
      <h5>Tens {props.points} pontos</h5>
      <div className="row justify-content-center m-4">
        {props.badges.map((badge) => {
          return (
            <div className="col-sm-12 col-md-6 col-lg-3">
              <div className="card m-2">
                <img
                  src={badge.image}
                  className="card-img-top"
                  style={{objectFit: "cover", width: "100%", height: "450px"}}
                  alt={"badge" + badge.id}
                />
                <div className="card-body">
                  <h5 class="card-title">{badge.name}</h5>
                  <p className="card-text mb-1 mt-3">
                    Multiplicador:{" "}
                    <span className="fw-bold">x{badge.multiplier}</span>
                  </p>
                  <p className="card-text mb-0">
                    Custo: <span className="fw-bold">{badge.cost} pontos</span>
                  </p>
                  {props.userBadges.find(
                    (userBadge) => userBadge === badge.id
                  ) ? (
                    <p className="card-text mt-4 mb-4 fw-bold">Adquirido</p>
                  ) : (
                    <button
                      className="btn btn-primary mt-4 purchase-button"
                      disabled={badge.cost > props.points}
                      onClick={() =>
                        props.onPurchaseClick(badge.id, badge.cost)
                      }
                    >
                      Comprar
                    </button>
                  )}
                </div>
              </div>
            </div>
          );
        })}
      </div>
      <button
        className="btn btn-lg btn-primary my-4"
        onClick={props.onCloseClick}
      >
        Voltar ao jogo
      </button>
    </div>
  );
}

export default Store;
