import React, { Component } from "react";

import api from "../api";
import { useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class NewCommunityCard extends Component {
  constructor(props) {
    super(props);

    this.handleInfoChange = this.handleInfoChange.bind(this);
    this.handlePointsChange = this.handlePointsChange.bind(this);
  }

  state = {
    info: "",
    points: 0,
  };

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

  handleClick() {
    // TODO: validar tudo

    const board = this.props.params.board;
    const info = this.state.info;
    const points = this.state.points;

    const payload = { board, info, points };

    api
      .newCommunityCard(payload)
      .then(() => {
        // TODO: voltar à página anterior
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

export default withParams(NewCommunityCard);
