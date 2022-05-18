import React, { Component } from "react";

import api from "../api";
import { useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class NewDeckCard extends Component {
  constructor(props) {
    super(props);

    this.handleDeckChange = this.handleDeckChange.bind(this);
    this.handleInfoChange = this.handleInfoChange.bind(this);
    this.handlePointsChange = this.handlePointsChange.bind(this);
    this.handleMoveChange = this.handleMoveChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  state = {
    deck: "communityCards",
    info: "",
    points: 0,
    move: 0,
  };

  handleDeckChange(e) {
    this.setState({
      deck: e.target.value,
    });
  }

  handleInfoChange(e) {
    this.setState({
      info: e.target.value,
    });
  }

  handlePointsChange(e) {
    this.setState({
      points: e.target.value,
    });
  }

  handleMoveChange(e) {
    this.setState({
      move: e.target.value,
    });
  }

  handleClick() {
    // TODO: validar tudo

    const board = this.props.params.board;
    const deck = this.state.deck;
    const info = this.state.info;
    const points = this.state.points;
    const move = this.state.move;

    const payload = { board, deck, info, points, move };

    api
      .newDeckCard(payload)
      .then(() => {
        window.location.href = `/admin/${this.props.params.board}`;
      })
      .catch((error) => {
        console.log(error.message);
      });
  }

  render() {
    return (
      <div className="row m-4">
        <div className="col-sm-12 col-md-8 col-lg-6 mx-auto">
          <div className="form-group row">
            <label for="deck" className="col-sm-2 col-form-label">
              Baralho
            </label>
            <div className="col-sm-10">
              <select
                class="form-select"
                name="deck"
                onChange={this.handleDeckChange}
              >
                <option selected={this.state.deck === "communityCards"} value="communityCards">Decisão do Senado</option>
                <option selected={this.state.deck === "chanceCards"} value="chanceCards">Sorte</option>
              </select>
            </div>
          </div>
          <div className="form-group row mt-4">
            <label for="info" className="col-sm-2 col-form-label">
              Descrição
            </label>
            <div className="col-sm-10">
              <textarea
                className="form-control"
                id="infoInput"
                name="info"
                onChange={this.handleInfoChange}
                value={this.state.info}
                rows="3"
              />
            </div>
          </div>
          <div className="form-group row mt-4">
            <label for="points" className="col-sm-2 col-form-label">
              Pontos imediatos
            </label>
            <div className="col-sm-10">
              <input
                type="number"
                className="form-control"
                id="pointsInput"
                name="points"
                onChange={this.handlePointsChange}
                value={this.state.points}
              />
            </div>
          </div>
          <div className="form-group row mt-4">
            <label for="move" className="col-sm-2 col-form-label">
              Mover (para mover para trás usar números negativos)
            </label>
            <div className="col-sm-10">
              <input
                type="number"
                className="form-control"
                id="moveInput"
                name="move"
                onChange={this.handleMoveChange}
                value={this.state.move}
              />
            </div>
          </div>
        </div>
        <div className="text-center mt-4">
          <button className="btn btn-lg btn-primary" onClick={this.handleClick}>
            Guardar carta
          </button>
        </div>
      </div>
    );
  }
}

export default withParams(NewDeckCard);
