import React, { Component } from "react";

class Menu extends Component {
  handleClick() {
    window.location.href = "play";
  }

  render() {
    return (
      <div>
        <button className="btn btn-primary btn-lg" onClick={this.handleClick}>
          Jogar
        </button>
      </div>
    );
  }
}

export default Menu;
