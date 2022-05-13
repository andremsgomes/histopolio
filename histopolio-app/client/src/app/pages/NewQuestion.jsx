import React, { Component } from "react";

import api from "../api";
import { useParams } from "react-router-dom";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class NewQuestion extends Component {
  constructor(props) {
    super(props);

    this.handleQuestionChange = this.handleQuestionChange.bind(this);
    this.handleImageChange = this.handleImageChange.bind(this);
    this.handleSelectChange = this.handleSelectChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  state = {
    question: "",
    image: "",
    answers: ["", "", "", "", "", "", "", "", "", ""],
    correctAnswer: 1,
  };

  handleQuestionChange(e) {
    this.setState({
      question: e.target.value,
    });
  }

  handleImageChange(e) {
    // TODO: importar imagens do computador
    this.setState({
      image: e.target.value,
    });
  }

  handleAnswerChange(e, i) {
    let newAnswers = [...this.state.answers];
    newAnswers[i] = e.target.value;

    this.setState({
      answers: newAnswers,
    });
  }

  handleSelectChange(e) {
    this.setState({
      correctAnswer: e.target.value,
    });
  }

  handleClick() {
    // TODO: validar tudo

    const board = this.props.params.board;
    const tileId = parseInt(this.props.params.tile);
    const question = this.state.question;
    const image = this.state.image;
    let answers = [];
    const correctAnswer = this.state.correctAnswer;

    this.state.answers.forEach((answer) => {
      if (answer.length > 0) answers.push(answer);
    });

    const payload = { board, tileId, question, image, answers, correctAnswer };

    api
      .newQuestion(payload)
      .then(() => {
        // TODO: window.location.href = `/admin/${board}/${tileId}/questions`;
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
            <label for="question" className="col-sm-2 col-form-label">
              Pergunta
            </label>
            <div className="col-sm-10">
              <input
                type="text"
                className="form-control"
                id="questionInput"
                name="question"
                onChange={this.handleQuestionChange}
                value={this.state.question}
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
          {this.state.image.length > 0 && (
            <div className="text-center mt-3">
              <img
                src={this.state.image}
                className="rounded"
                alt="question"
                width="600"
              />
            </div>
          )}
          <div className="mt-4">
            {this.state.answers.map((answer, i) => {
              return (
                <div className="form-group row mt-3" key={i}>
                  <label for={"answer" + i} className="col-sm-2 col-form-label">
                    Resposta {i + 1}
                  </label>
                  <div className="col-sm-10">
                    <input
                      type="text"
                      className="form-control"
                      name={"answer" + i}
                      onChange={(e) => this.handleAnswerChange(e, i)}
                      value={answer}
                    />
                  </div>
                </div>
              );
            })}
          </div>
          <div className="form-group row mt-4">
            <label for="correctAnswer" className="col-sm-2 col-form-label">
              Resposta correta
            </label>
            <div className="col-sm-10">
              <select
                class="form-select"
                name="correcAnswer"
                onChange={this.handleSelectChange}
              >
                {this.state.answers.map((_, i) => {
                  return (
                    <option selected={i === 0} value={i + 1}>
                      Resposta {i + 1}
                    </option>
                  );
                })}
              </select>
            </div>
          </div>
        </div>
        <div className="text-center mt-4">
          <button className="btn btn-lg btn-primary" onClick={this.handleClick}>
            Guardar pergunta
          </button>
        </div>
      </div>
    );
  }
}

export default withParams(NewQuestion);
