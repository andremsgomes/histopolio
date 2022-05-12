import React, { Component } from "react";
import { Link } from "react-router-dom";

import api from "../api";

class Admin extends Component {
  state = {
    saves: [],
  };

  componentDidMount() {
    api
      .saves("Histopolio")
      .then((res) => {
        this.setState({
          saves: res.data,
        });
      })
      .catch((error) => {
        console.log(error.message);
      });
  }

  render() {
    return (
      <div className="text-center m-4">
        {this.state.saves.length > 0 && (
          <h3 className="mb-3">Dados guardados:</h3>
        )}
        {this.state.saves.map((save) => {
          const saveName = save.substring(0, save.indexOf(".json"));

          return (
            <Link
              to={"/admin/edit/Histopolio/" + saveName}
              style={{ textDecoration: "none" }}
            >
              <div className="card">
                <div className="card-body">
                  <h4 className="card-title">{saveName}</h4>
                </div>
              </div>
            </Link>
          );
        })}
        <Link to="/admin/edit/Histopolio" style={{ textDecoration: "none" }}>
          <button className="btn btn-lg btn-primary my-4">
            Editar tabuleiro
          </button>
        </Link>
      </div>
    );
  }
}

export default Admin;
