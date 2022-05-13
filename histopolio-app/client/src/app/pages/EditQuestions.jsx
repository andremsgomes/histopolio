import React, { Component } from "react";

import api from "../api";
import { Link, useParams } from "react-router-dom";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan } from "@fortawesome/free-solid-svg-icons";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class EditQuestions extends Component {
  state = {
    questions: [],
  };

  componentDidMount() {
    api
      .questionsData(this.props.params.board, this.props.params.tile)
      .then((res) => {
        console.log(res);
        this.setState({
          questions: res.data,
        });
      })
      .catch((error) => {
        console.log(error.message);
      });
  }

  render() {
    return (
      <div className="text-center mt-4">
        <h1>Tabela de perguntas</h1>
        <h4>
          {this.props.params.board} - Casa {this.props.params.tile}
        </h4>
        <table className="table table-hover mt-4">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Pergunta</th>
              <th scope="col">Opção Correta</th>
              <th scope="col"></th>
              <th scope="col"></th>
            </tr>
          </thead>
          <tbody>
            {this.state.questions.map((question) => {
              return (
                <tr>
                  <th scope="row">{question.id}</th>
                  <td>{question.question}</td>
                  <td>{question.answers[question.correctAnswer - 1]}</td>
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
        <Link
          to={
            "/admin/" +
            this.props.params.board +
            "/" +
            this.props.params.tile +
            "/questions/new"
          }
          style={{ textDecoration: "none" }}
        >
          <button className="btn btn-lg btn-primary my-4">
            Adicionar pergunta
          </button>
        </Link>
      </div>
    );
  }
}

export default withParams(EditQuestions);
