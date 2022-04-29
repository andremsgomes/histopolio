import React, { Component, useContext } from "react";
import { w3cwebsocket } from "websocket";
import AuthContext from "../context/AuthContext";

class Play extends Component {
  constructor(props) {
    super(props);

    this.client = new w3cwebsocket("ws://localhost:8080");
  }

  static contextType = AuthContext;

  state = {
    gameStarted: false,
    showQuestion: false,
    question: "",
    answers: [],
  };

  componentDidMount() {
    this.client.onopen = () => {
      console.log("WebSocket Client Connected");

      this.sendIdentificationMessage();
      this.sendRequestGameStatusMessage();
    };

    this.client.onmessage = (message) => {
      console.log(message.data);
      const dataReceived = JSON.parse(message.data);

      this.processDataReceived(dataReceived);
    };
  }

  sendIdentificationMessage() {
    const { user } = this.context;

    const dataToSend = {
      type: "identification",
      platform: "react",
      id: user.id
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  sendRequestGameStatusMessage() {
    const dataToSend = {
      type: "game status",
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  sendToServer(message) {
    this.client.send(message);
  }

  processDataReceived(dataReceived) {
    const command = dataReceived["type"];

    switch (command) {
      case "question":
        this.handleQuestionReceived(dataReceived);
        break;
      case "game status":
        this.handleGameStatusReceived(dataReceived);
        break;
      default:
        console.log("Unknown message: " + dataReceived);
    }
  }

  handleQuestionReceived(dataReceived) {
    this.setState({
      question: dataReceived["question"],
      answers: dataReceived["answers"],
      showQuestion: true,
    });
  }

  handleGameStatusReceived(dataReceived) {
    this.setState({
      gameStarted: dataReceived["gameStarted"],
    });

    if (this.state.gameStarted) {
      this.sendJoinGameMessage();
    }
  }

  sendJoinGameMessage() {
    const dataToSend = {
      type: "join game",
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  handleAnswer(answerIndex) {
    this.setState({ showQuestion: false });

    const answer = answerIndex + 1;

    const dataToSend = {
      type: "answer",
      answer: answer,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  render() {
    if (this.state.gameStarted) {
      return (
        <div>
          {this.state.showQuestion ? (
            <div>
              <h1>{this.state.question}</h1>
              {this.state.answers.map((answer, index) => (
                <button
                  className="btn btn-secondary btn-lg"
                  onClick={() => {
                    this.handleAnswer(index);
                  }}
                >
                  {answer}
                </button>
              ))}
            </div>
          ) : (
            <h2>Espera pela tua vez!</h2>
          )}
        </div>
      );
    } else {
      return (
        <div>
          <h2>Espera pelo início do jogo!</h2>
        </div>
      );
    }
  }
}

export default Play;
