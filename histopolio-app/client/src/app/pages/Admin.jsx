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
        {this.state.saves.map((save) => {
          const saveName = save.substring(0, save.indexOf(".json"));

          return (
            <Link
              to={"/admin/Histopolio/" + saveName}
              style={{ textDecoration: "none" }}
            >
              <div class="card">
                <div class="card-body">
                  <h4 class="card-title">{saveName}</h4>
                </div>
              </div>
            </Link>
          );
        })}
      </div>
    );
  }
}

export default Admin;
