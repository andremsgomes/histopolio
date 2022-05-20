import React, { Component } from "react";

import api from "../api";
import { Link, useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class EditSave extends Component {
  constructor(props) {
    super(props);

    this.handlePositionChange = this.handlePositionChange.bind(this);
    this.handlePointsChange = this.handlePointsChange.bind(this);
    this.handleNumTurnsChange = this.handleNumTurnsChange.bind(this);
    this.handleAnswersChange = this.handleAnswersChange.bind(this);
    this.handleCorrectAnswersChange = this.handleCorrectAnswersChange.bind(
      this
    );
    this.handleClick = this.handleClick.bind(this);
  }

  state = {
    players: [],
    alertType: "",
    alertMessage: "",
  };

  componentDidMount() {
    api
      .savedData(this.props.params.board, this.props.params.save)
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

  handlePositionChange(e, userId) {
    const newPlayers = this.state.players.map((player) => {
      if (player.userId === userId) {
        player.position = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handlePointsChange(e, userId) {
    const newPlayers = this.state.players.map((player) => {
      if (player.userId === userId) {
        player.points = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handleNumTurnsChange(e, userId) {
    const newPlayers = this.state.players.map((player) => {
      if (player.userId === userId) {
        player.numTurns = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handleAnswersChange(e, userId) {
    const newPlayers = this.state.players.map((player) => {
      if (player.userId === userId) {
        player.totalAnswers = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handleCorrectAnswersChange(e, userId) {
    const newPlayers = this.state.players.map((player) => {
      if (player.userId === userId) {
        player.correctAnswers = parseInt(e.target.value);
      }

      return player;
    });

    this.setState({
      players: newPlayers,
    });
  }

  handleClick() {
    const board = this.props.params.board;
    const save = this.props.params.save;
    const savedData = this.state.players;
    const payload = { board, save, savedData };

    api
      .updateSave(payload)
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
      <div className="mt-4">
        <nav aria-label="breadcrumb" className="m-4">
          <ol className="breadcrumb">
            <li className="breadcrumb-item">
              <Link to="/admin">Admin</Link>
            </li>
            <li className="breadcrumb-item active" aria-current="page">
              {this.props.params.save}
            </li>
          </ol>
        </nav>
        <div className="text-center mt-4">
          {this.state.alertMessage.length > 0 && (
            <div className={"alert alert-" + this.state.alertType} role="alert">
              {this.state.alertMessage}
            </div>
          )}
          <h1>
            {this.props.params.board} - {this.props.params.save}
          </h1>
          <h4>Tabela de jogadores</h4>
          <div className="table-responsive">
            <table className="table table-hover mt-4">
              <thead>
                <tr>
                  <th scope="col">ID</th>
                  <th scope="col">Nome</th>
                  <th scope="col">Email</th>
                  <th scope="col">Posição no tabuleiro</th>
                  <th scope="col">Número de jogadas</th>
                  <th scope="col">Pontuação</th>
                  <th scope="col">Total de respostas</th>
                  <th scope="col">Respostas corretas</th>
                  <th scope="col">% de respostas corretas</th>
                </tr>
              </thead>
              <tbody>
                {this.state.players.map((player) => {
                  const percCorrect =
                    !player.totalAnswers || player.totalAnswers === 0
                      ? "-"
                      : Math.round(
                          (player.correctAnswers / player.totalAnswers) *
                            100 *
                            100
                        ) /
                          100 +
                        "%";

                  return (
                    <tr key={player.userId}>
                      <th scope="row">{player.userId}</th>
                      <td>{player.name}</td>
                      <td>{player.email}</td>
                      <td>
                        <input
                          id={"position" + player.userId}
                          className="table-input-number"
                          onChange={(e) =>
                            this.handlePositionChange(e, player.userId)
                          }
                          type="number"
                          value={player.position}
                        />
                      </td>
                      <td>
                        <input
                          id={"numTurns" + player.userId}
                          className="table-input-number"
                          onChange={(e) =>
                            this.handleNumTurnsChange(e, player.userId)
                          }
                          type="number"
                          value={player.numTurns}
                        />
                      </td>
                      <td>
                        <input
                          id={"points" + player.userId}
                          className="table-input-number"
                          onChange={(e) =>
                            this.handlePointsChange(e, player.userId)
                          }
                          type="number"
                          value={player.points}
                        />
                      </td>
                      <td>
                        <input
                          id={"answers" + player.userId}
                          className="table-input-number"
                          onChange={(e) =>
                            this.handleAnswersChange(e, player.userId)
                          }
                          type="number"
                          value={player.totalAnswers}
                        />
                      </td>
                      <td>
                        <input
                          id={"correctAnswers" + player.userId}
                          className="table-input-number"
                          onChange={(e) =>
                            this.handleCorrectAnswersChange(e, player.userId)
                          }
                          type="number"
                          value={player.correctAnswers}
                        />
                      </td>
                      <td>{percCorrect}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
          <button
            className="btn btn-lg btn-primary my-4"
            onClick={this.handleClick}
          >
            Guardar alterações
          </button>
        </div>
      </div>
    );
  }
}

export default withParams(EditSave);
