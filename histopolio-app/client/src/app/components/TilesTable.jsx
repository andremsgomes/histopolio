import React from "react";
import { Link } from "react-router-dom";

function TilesTable(props) {
  return (
    <div>
      <h4 className="mt-4">{props.title}</h4>
      <table className="table table-hover mt-3">
        <thead>
          <tr>
            <th scope="col">Posição</th>
            <th scope="col">Nome</th>
            <th scope="col">{props.pointsCol}</th>
            <th scope="col">{props.content}</th>
          </tr>
        </thead>
        <tbody>
          {props.tiles.map((tile) => {
            return (
              <tr>
                <th scope="row">{tile.id}</th>
                <td>
                  <input
                    id={"name" + tile.id}
                    onChange={props.onNameChange}
                    type="text"
                    value={tile.tileName}
                  />
                </td>
                <td>
                  <input
                    id={"points" + tile.id}
                    onChange={props.onPointsChange}
                    type="number"
                    value={tile.points}
                  />
                </td>
                <td>
                  <Link
                    to={"/admin/" + props.board + "/" + tile.id + props.contentLink}
                  >
                    {props.content === "Perguntas" ? (
                      <div>
                        {tile.questions} pergunta{tile.questions !== 1 && "s"}
                      </div>
                    ) : (
                      <div>
                        {tile.cards} carta{tile.cards !== 1 && "s"}
                      </div>
                    )}
                  </Link>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default TilesTable;
