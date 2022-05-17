import React, { Component } from "react";

import api from "../api";
import { Link, useParams } from "react-router-dom";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan } from "@fortawesome/free-solid-svg-icons";

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
        <h4 className="mt-4">Casas de perguntas</h4>
        {this.state.board && (
          <div>
            <table className="table table-hover mt-3">
              <thead>
                <tr>
                  <th scope="col">Posição</th>
                  <th scope="col">Nome</th>
                  <th scope="col">Pontos</th>
                  <th scope="col">Perguntas</th>
                </tr>
              </thead>
              <tbody>
                {this.state.board.groupPropertyTiles.map((tile) => {
                  return (
                    <tr>
                      <th scope="row">{tile.id}</th>
                      <td>
                        <input
                          id={"name" + tile.id}
                          onChange={this.handleNameChange}
                          type="text"
                          value={tile.tileName}
                        />
                      </td>
                      <td>
                        <input
                          id={"points" + tile.id}
                          onChange={this.handlePointsChange}
                          type="number"
                          value={tile.points}
                        />
                      </td>
                      <td>
                        <Link
                          to={
                            "/admin/" +
                            this.props.params.board +
                            "/" +
                            tile.id +
                            "/questions"
                          }
                        >
                          {tile.questions} pergunta{tile.questions !== 1 && "s"}
                        </Link>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
            <button
              className="btn btn-lg btn-primary my-4"
              onClick={this.handleClick}
            >
              Guardar alterações
            </button>
            <h4 className="mt-4">Cartas Decisões do Senado</h4>
            {this.state.board.communityCards.length > 0 && (
              <table className="table table-hover mt-3">
                <thead>
                  <tr>
                    <th scope="col">#</th>
                    <th scope="col">Info</th>
                    <th scope="col">Pontos imediatos</th>
                    <th scope="col"></th>
                    <th scope="col"></th>
                  </tr>
                </thead>
                <tbody>
                  {this.state.board.communityCards.map((card) => {
                    return (
                      <tr>
                        <th scope="row">{card.id}</th>
                        <td>{card.info}</td>
                        <td>{card.points}</td>
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
              to={
                "/admin/" +
                this.props.params.board +
                "/cards/community_cards/new"
              }
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
