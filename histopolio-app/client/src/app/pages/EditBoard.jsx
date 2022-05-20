import React, { Component } from "react";

import api from "../api";
import { Link, useParams } from "react-router-dom";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan } from "@fortawesome/free-solid-svg-icons";
import TilesTable from "../components/TilesTable";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class EditBoard extends Component {
  constructor(props) {
    super(props);

    this.handleNameChange = this.handleNameChange.bind(this);
    this.handlePointsChange = this.handlePointsChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  state = {
    board: null,
  };

  componentDidMount() {
    api
      .boardData(this.props.params.board)
      .then((res) => {
        this.setState({
          board: res.data,
        });
      })
      .catch((error) => {
        console.log(error.message);
      });
  }

  handleNameChange(e) {
    const tileId = parseInt(e.target.id.substring(4));

    const newGroupPropertyTiles = this.state.board.groupPropertyTiles.map(
      (tile) => {
        if (tile.id === tileId) {
          tile.tileName = e.target.value;
        }

        return tile;
      }
    );

    let newBoard = this.state.board;
    newBoard.groupPropertyTiles = newGroupPropertyTiles;

    this.setState({
      board: newBoard,
    });
  }

  handlePointsChange(e) {
    const tileId = parseInt(e.target.id.substring(6));

    const newGroupPropertyTiles = this.state.board.groupPropertyTiles.map(
      (tile) => {
        if (tile.id === tileId) {
          tile.points = parseInt(e.target.value);
        }

        return tile;
      }
    );

    let newBoard = this.state.board;
    newBoard.groupPropertyTiles = newGroupPropertyTiles;

    this.setState({
      board: newBoard,
    });
  }

  handleClick() {
    let boardData = JSON.parse(JSON.stringify(this.state.board));

    boardData.groupPropertyTiles.forEach((tile) => {
      delete tile.questions;
    });

    boardData.payTiles.forEach((tile) => {
      delete tile.questions;
    });

    boardData.stationTiles.forEach((tile) => {
      delete tile.cards;
    });

    delete boardData.communityCards;
    delete boardData.chanceCards;

    const payload = { boardData };

    api
      .updateBoard(payload)
      .then(() => {})
      .catch((error) => {
        console.log(error.message);
      });
  }

  render() {
    return (
      <div className="text-center mt-4">
        <h1>{this.props.params.board}</h1>
        {this.state.board && (
          <div>
            <TilesTable
              title="Casas de perguntas"
              pointsCol="Pontos"
              content="Perguntas"
              tiles={this.state.board.groupPropertyTiles}
              onNameChange={this.handleNameChange}
              onPointsChange={this.handlePointsChange}
              board={this.props.params.board}
              contentLink="/questions"
            />
            <TilesTable
              title="Casas de azar"
              pointsCol="Pontos a retirar"
              content="Perguntas"
              tiles={this.state.board.payTiles}
              onNameChange={this.handleNameChange}
              onPointsChange={this.handlePointsChange}
              board={this.props.params.board}
              contentLink="/questions"
            />
            <TilesTable
              title="Estações de treino"
              pointsCol="Pontos"
              content="Cartas"
              tiles={this.state.board.stationTiles}
              onNameChange={this.handleNameChange}
              onPointsChange={this.handlePointsChange}
              board={this.props.params.board}
              contentLink="/train_cards"
            />
            <button
              className="btn btn-lg btn-primary my-4"
              onClick={this.handleClick}
            >
              Guardar alterações
            </button>
            <h4 className="mt-4">Cartas</h4>
            {this.state.board.communityCards.length > 0 && (
              <table className="table table-hover mt-3">
                <thead>
                  <tr>
                    <th scope="col">#</th>
                    <th scope="col">Tipo</th>
                    <th scope="col">Descrição</th>
                    <th scope="col">Pontos imediatos</th>
                    <th scope="col">Ação</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                  </tr>
                </thead>
                <tbody>
                  {this.state.board.communityCards.map((card) => {
                    return (
                      <tr>
                        <th scope="row">{card.id}</th>
                        <td>Decisão do Senado</td>
                        <td>{card.info}</td>
                        <td>{card.points}</td>
                        <td>
                          {card.action === "none" ? (
                            <div>Sem ação</div>
                          ) : card.action === "move" ? (
                            <div>Mover {card.actionValue}</div>
                          ) : (
                            <div>Mover para casa {card.actionValue}</div>
                          )}
                        </td>
                        <td>
                          <FontAwesomeIcon icon={faPencil} />
                        </td>
                        <td>
                          <FontAwesomeIcon icon={faTrashCan} />
                        </td>
                      </tr>
                    );
                  })}
                  {this.state.board.chanceCards.map((card) => {
                    return (
                      <tr>
                        <th scope="row">{card.id}</th>
                        <td>Sorte</td>
                        <td>{card.info}</td>
                        <td>{card.points}</td>
                        <td>
                          {card.action === "none" ? (
                            <div>Sem ação</div>
                          ) : card.action === "move" ? (
                            <div>Mover {card.actionValue} casas</div>
                          ) : (
                            <div>Mover para a casa {card.actionValue}</div>
                          )}
                        </td>
                        <td>
                          <FontAwesomeIcon icon={faPencil} />
                        </td>
                        <td>
                          <FontAwesomeIcon icon={faTrashCan} />
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            )}
            <Link
              to={"/admin/" + this.props.params.board + "/cards/deck/new"}
              style={{ textDecoration: "none" }}
            >
              <button
                className="btn btn-lg btn-primary my-4"
                onClick={this.handleClick}
              >
                Adicionar carta
              </button>
            </Link>
          </div>
        )}
      </div>
    );
  }
}

export default withParams(EditBoard);
