import React, { Component } from "react";

import api from "../api";
import { Link, useParams } from "react-router-dom";

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
        <h4>Tabela de casas</h4>
        <table className="table table-hover mt-4">
          <thead>
            <tr>
              <th scope="col">Posição</th>
              <th scope="col">Nome</th>
              <th scope="col">Pontos</th>
              <th scope="col">Perguntas</th>
            </tr>
          </thead>
          <tbody>
            {this.state.board &&
              this.state.board.groupPropertyTiles.map((tile) => {
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
                        {tile.questions} pergunta{tile.questions !== 1 && ("s")}
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
      </div>
    );
  }
}

export default withParams(EditBoard);
