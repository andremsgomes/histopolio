import React, { Component } from "react";

import api from "../api";
import { useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class NewTrainCard extends Component {
  constructor(props) {
    super(props);

    this.handleInfoChange = this.handleInfoChange.bind(this);
    this.handleContentChange = this.handleContentChange.bind(this);
  }

  state = {
    info: "",
    content: "",
  };

  handleInfoChange(e) {
    this.setState({
      info: e.target.value,
    });
  }

  handleContentChange(e) {
    this.setState({
      content: e.target.value,
    });
  }

  handleClick() {
    // TODO: validar tudo

    const board = this.props.params.board;
    const tileId = this.props.params.tile;
    const info = this.state.info;
    const content = this.state.content;

    const payload = { board, tileId, info, content };

    api
      .newCommunityCard(payload)
      .then(() => {
        window.location.href = `/admin/${this.props.params.board}/${this.props.params.tile}/train_cards`;
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
            <label for="content" className="col-sm-2 col-form-label">
              Link para o conteúdo
            </label>
            <div className="col-sm-10">
              <input
                type="text"
                className="form-control"
                id="contentInput"
                name="content"
                onChange={this.handleContentChange}
                value={this.state.content}
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

export default withParams(NewTrainCard);
