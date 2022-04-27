import React, { Component } from "react";
import { w3cwebsocket } from 'websocket';

class Play extends Component {
  constructor(props) {
    super(props);

    this.client = new w3cwebsocket("ws://localhost:8080");
  }

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
    const dataToSend = {
      type: "identification",
      id: "react",
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
      type: "join game"
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
              <p>{this.state.question}</p>
              {this.state.answers.map((answer, index) => (
                <button
                  onClick={() => {
                    this.handleAnswer(index);
                  }}
                >
                  {answer}
                </button>
              ))}
            </div>
          ) : (
            <p>Espera pela tua vez!</p>
          )}
        </div>
      );
    } else {
      return (
        <div>
          <p>Espera pelo início do jogo!</p>
        </div>
      );
    }
  }
}

export default Play;
