import React, { Component } from "react";

import api from "../api";
import { useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class EditBoard extends Component {
  state = {
    board: null,
  };

  componentDidMount() {
    api
      .boardData(this.props.params.board)
      .then((res) => {
        console.log(res);
        this.setState({
          board: res.data,
        });
      })
      .catch((error) => {
        console.log(error.message);
      });
  }

  render() {
    return (
      <div className="text-center mt-4">
        <h1>Tabela de casas</h1>
        <h4>{this.props.params.board}</h4>
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
                    <td>{tile.tileName}</td>
                    <td>{tile.points}</td>
                    <td>{tile.questions.length} perguntas</td>
                  </tr>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  }
}

export default withParams(EditBoard);
