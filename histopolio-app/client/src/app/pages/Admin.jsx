import React, { Component } from "react";

import api from "../api";

import AuthContext from "../context/AuthContext";

class Admin extends Component {
  constructor(props) {
    super(props);

    this.handlePositionChange = this.handlePositionChange.bind(this);
    this.handlePointsChange = this.handlePointsChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  static contextType = AuthContext;

  state = {
    players: [],
    alertType: "",
    alertMessage: "",
  };

  componentDidMount() {
    api
      .savedData("Histopolio", "Turma1")
      .then((res) => {
        this.setState({
          players: res.data,
        });
      })
      .catch((error) => {
        console.log(error.message);
        this.setState({
          alertType: "danger",
          alertMessage: error.message,
        });
      });
  }

  handlePositionChange(e) {
    const playerId = parseInt(e.target.id.substring(8));

    const newPlayers = this.state.players.map((player) => {
      if (player.userId === playerId) {
        player.position = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handlePointsChange(e) {
    const playerId = parseInt(e.target.id.substring(6));

    const newPlayers = this.state.players.map((player) => {
      if (player.userId === playerId) {
        player.points = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handleClick() {
    const board = "Histopolio";
    const save = "Turma1";
    const savedData = this.state.players;
    const payload = { board, save, savedData };

    api
      .updateData(payload)
      .then(() => {
        this.setState({
          alertType: "success",
          alertMessage: "Dados atualizados com sucesso.",
        });
      })
      .catch((error) => {
        console.log(error.message);
        this.setState({
          alertType: "danger",
          alertMessage: error.message,
        });
      });
  }

  render() {
    return (
      <div className="text-center mt-4">
        {this.state.alertMessage.length > 0 && (
          <div className={"alert alert-" + this.state.alertType} role="alert">
            {this.state.alertMessage}
          </div>
        )}
        <h1>Tabela de jogadores</h1>
        <table className="table table-hover mt-4">
          <thead>
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Nome</th>
              <th scope="col">Email</th>
              <th scope="col">Posição no tabuleiro</th>
              <th scope="col">Pontuação</th>
            </tr>
          </thead>
          <tbody>
            {this.state.players.map((player) => {
              return (
                <tr>
                  <th scope="row">{player.userId}</th>
                  <th>{player.name}</th>
                  <th>{player.email}</th>
                  <th>
                    <input
                      id={"position" + player.userId}
                      onChange={this.handlePositionChange}
                      type="number"
                      value={player.position}
                    />
                  </th>
                  <th>
                    <input
                      id={"points" + player.userId}
                      onChange={this.handlePointsChange}
                      type="number"
                      value={player.points}
                    />
                  </th>
                </tr>
              );
            })}
          </tbody>
        </table>
        <button
          className="btn btn-lg btn-primary mt-4"
          onClick={this.handleClick}
        >
          Guardar alterações
        </button>
      </div>
    );
  }
}

export default Admin;
