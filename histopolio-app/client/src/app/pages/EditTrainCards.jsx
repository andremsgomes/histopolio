import React, { Component } from "react";

import api from "../api";
import { Link, useParams } from "react-router-dom";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencil, faTrashCan } from "@fortawesome/free-solid-svg-icons";

function withParams(Component) {
  return (props) => <Component {...props} params={useParams()} />;
}

class EditTrainCards extends Component {
  state = {
    cards: [],
  };

  componentDidMount() {
    api
      .trainCardsData(this.props.params.board, this.props.params.tile)
      .then((res) => {
        console.log(res);
        this.setState({
          cards: res.data,
        });
      })
      .catch((error) => {
        console.log(error.message);
      });
  }

  render() {
    return (
      <div className="text-center mt-4">
        <h1>
          {this.props.params.board} - Casa {this.props.params.tile}
        </h1>
        <h4>Tabela de cartas</h4>
        <table className="table table-hover mt-4">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Descrição</th>
              <th scope="col">Conteúdo</th>
              <th scope="col"></th>
              <th scope="col"></th>
            </tr>
          </thead>
          <tbody>
            {this.state.cards.map((card) => {
              return (
                <tr>
                  <th scope="row">{card.id}</th>
                  <td>{card.info}</td>
                  <td>{card.content}</td>
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
            "/train_cards/new"
          }
          style={{ textDecoration: "none" }}
        >
          <button className="btn btn-lg btn-primary my-4">
            Adicionar carta
          </button>
        </Link>
      </div>
    );
  }
}

export default withParams(EditTrainCards);