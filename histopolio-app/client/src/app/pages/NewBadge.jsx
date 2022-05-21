import React, { Component } from "react";

import api from "../api";
import { useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class NewBadge extends Component {
  constructor(props) {
    super(props);

    this.hadnleNameChange = this.hadnleNameChange.bind(this);
    this.handleImageChange = this.handleImageChange.bind(this);
    this.handleMultiplierChange = this.handleMultiplierChange.bind(this);
    this.handleCostChange = this.handleCostChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  state = {
    name: "",
    image: "",
    multiplier: 1,
    cost: 0,
  };

  hadnleNameChange(e) {
    this.setState({
      name: e.target.value,
    });
  }

  handleImageChange(e) {
    this.setState({
      image: e.target.value,
    });
  }

  handleMultiplierChange(e) {
    this.setState({
      multiplier: e.target.value,
    });
  }

  handleCostChange(e) {
    this.setState({
      cost: e.target.value,
    });
  }

  handleClick() {
    // TODO: validar tudo

    const board = this.props.params.board;
    const name = this.state.name;
    const multiplier = this.state.multiplier;
    const cost = this.state.cost;

    // image validation
    const imageToSend =
      this.state.image.length > 0 ? this.state.image : "https://www.linkpicture.com/q/badge_9.png";

    const payload = { board, name, imageToSend, multiplier, cost };

    api
      .newBadge(payload)
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
            <label for="name" className="col-sm-2 col-form-label">
              Nome
            </label>
            <div className="col-sm-10">
              <input
                type="text"
                className="form-control"
                id="nameInput"
                name="name"
                onChange={this.hadnleNameChange}
                value={this.state.name}
              />
            </div>
          </div>
          <div className="form-group row mt-4">
            <label for="image" className="col-sm-2 col-form-label">
              Link da imagem
            </label>
            <div className="col-sm-10">
              <input
                type="text"
                className="form-control"
                id="imageInput"
                name="image"
                onChange={this.handleImageChange}
                value={this.state.image}
              />
            </div>
          </div>
          <div className="text-center mt-3">
            {this.state.image.length > 0 ? (
              <img
                src={this.state.image}
                alt="Badge"
                className="rounded-circle border"
                style={{ objectFit: "cover", width: "250px", height: "250px" }}
              />
            ) : (
              <img
                src="https://www.linkpicture.com/q/badge_9.png"
                alt="Badge"
                className="rounded-circle border"
                style={{ objectFit: "cover", width: "250px", height: "250px" }}
              />
            )}
          </div>
          <div className="form-group row mt-4">
            <label for="multiplier" className="col-sm-2 col-form-label">
              Multiplicador
            </label>
            <div className="col-sm-10">
              <input
                type="number"
                className="form-control"
                id="multiplierInput"
                name="multiplier"
                onChange={this.handleMultiplierChange}
                value={this.state.multiplier}
              />
            </div>
          </div>
          <div className="form-group row mt-4">
            <label for="cost" className="col-sm-2 col-form-label">
              Custo (pontos)
            </label>
            <div className="col-sm-10">
              <input
                type="text"
                className="form-control"
                id="costInput"
                name="cost"
                onChange={this.handleCostChange}
                value={this.state.cost}
              />
            </div>
          </div>
        </div>
        <div className="text-center mt-4">
          <button className="btn btn-lg btn-primary" onClick={this.handleClick}>
            Guardar troféu
          </button>
        </div>
      </div>
    );
  }
}

export default withParams(NewBadge);
