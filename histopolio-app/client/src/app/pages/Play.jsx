import React, { Component } from "react";

import { w3cwebsocket } from "websocket";
import ReactDice from "react-dice-complete";
import "react-dice-complete/dist/react-dice-complete.css";

import AuthContext from "../context/AuthContext";

class Play extends Component {
  constructor(props) {
    super(props);

    this.client = new w3cwebsocket("ws://localhost:8080");

    this.handleDiceClick = this.handleDiceClick.bind(this);
  }

  static contextType = AuthContext;

  state = {
    gameStarted: false,
    showDice: false,
    rollTime: 0,
    diceRolled: false,
    showQuestion: false,
    question: "",
    answers: [],
    points: 0,
    position: 0,
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
      id: user.id,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  sendRequestGameStatusMessage() {
    const { user } = this.context;

    const dataToSend = {
      type: "game status",
      userId: user.id,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  sendToServer(message) {
    this.client.send(message);
  }

  processDataReceived(dataReceived) {
    const command = dataReceived["type"];

    switch (command) {
      case "game status":
        this.handleGameStatusReceived(dataReceived);
        break;
      case "turn":
        this.handleTurnReceived();
        break;
      case "info shown":
        this.hideDice();
        break;
      case "question":
        this.handleQuestionReceived(dataReceived);
        break;
      default:
        console.log("Unknown message: " + dataReceived);
    }
  }

  handleGameStatusReceived(dataReceived) {
    this.setState({
      gameStarted: dataReceived["gameStarted"],
      points: dataReceived["playerData"]["points"],
      position: dataReceived["playerData"]["position"],
    });

    if (this.state.gameStarted) {
      this.sendJoinGameMessage();
    }
  }

  sendJoinGameMessage() {
    const { user } = this.context;

    const dataToSend = {
      type: "join game",
      userId: user.id,
      name: user.name
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  handleTurnReceived() {
    this.setState({
      showDice: true,
    });
  }

  hideDice() {
    this.setState({
      showDice: false,
      rollTime: 0,
      diceRolled: false,
    });
  }

  handleQuestionReceived(dataReceived) {
    this.setState({
      question: dataReceived["questionData"]["question"],
      answers: dataReceived["questionData"]["answers"],
      showQuestion: true,
    });

    this.hideDice();
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

  rollDoneCallback(num) {
    const dataToSend = {
      type: "dice result",
      result: num,
      rollTime: this.state.rollTime * 1000,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  handleDiceClick() {
    if (!this.state.diceRolled) {
      const rollTime = Math.random() * 2 + 1;

      this.setState({
        rollTime: rollTime,
        diceRolled: true,
      });

      this.reactDice.rollAll();
    }
  }

  render() {
    if (this.state.gameStarted) {
      return (
        <div>
          {this.state.showDice ? (
            <div className="text-center">
              <h4 className="mb-4">Lança o dado!</h4>
              <div className="mt-4" onClick={this.handleDiceClick}>
                <ReactDice
                  numDice={1}
                  faceColor="#ffF"
                  dotColor="#000000"
                  outline={true}
                  dieSize={200}
                  rollTime={this.state.rollTime}
                  rollDone={(num) => this.rollDoneCallback(num)}
                  disableIndividual={true}
                  ref={(dice) => (this.reactDice = dice)}
                />
              </div>
            </div>
          ) : (
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
