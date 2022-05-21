import React, { Component } from "react";

import { w3cwebsocket } from "websocket";
import ReactDice from "react-dice-complete";
import { Link } from "react-router-dom";
import "react-dice-complete/dist/react-dice-complete.css";

import Wait from "../components/Wait";
import Question from "../components/Question";
import Store from "../components/Store";

class Play extends Component {
  constructor(props) {
    super(props);

    this.client = new w3cwebsocket("ws://localhost:8080");

    this.handleDiceClick = this.handleDiceClick.bind(this);
    this.handleAnswer = this.handleAnswer.bind(this);
    this.handleContentClick = this.handleContentClick.bind(this);
    this.handleStoreClick = this.handleStoreClick.bind(this);
    this.handleCloseStoreClick = this.handleCloseStoreClick.bind(this);
    this.handleBadgePurchased = this.handleBadgePurchased.bind(this);
  }

  state = {
    gameStarted: false,
    showDice: false,
    rollTime: 0,
    diceRolled: false,
    question: null,
    content: "",
    storeOpen: false,
    badges: [],
    points: 0,
    position: 0,
    rank: 0,
    userBadges: [],
  };

  componentDidMount() {
    this.client.onopen = () => {
      console.log("WebSocket Client Connected");

      this.sendIdentificationMessage();
      this.loadBadges();
      this.sendRequestGameStatusMessage();
    };

    this.client.onmessage = (message) => {
      console.log(message.data);
      const dataReceived = JSON.parse(message.data);

      this.processDataReceived(dataReceived);
    };
  }

  sendIdentificationMessage() {
    const user = JSON.parse(sessionStorage.getItem("user"));

    const dataToSend = {
      type: "identification",
      platform: "react",
      id: user.id,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  sendRequestGameStatusMessage() {
    const user = JSON.parse(sessionStorage.getItem("user"));

    const dataToSend = {
      type: "game status",
      userId: user.id,
      board: "Histopolio", // TODO: ter no url
      saveFile: "Turma1.json", // TODO: retirar
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  loadBadges() {
    const dataToSend = {
      type: "load badges",
      board: "Histopolio", // TODO: ter no url
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
      case "badges":
        this.handleBadgesReceived(dataReceived);
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
      case "update":
        this.handleUpdate(dataReceived);
        break;
      case "content":
        this.handleContentReceived(dataReceived);
        break;
      default:
        console.log("Unknown message: " + dataReceived);
    }
  }

  handleGameStatusReceived(dataReceived) {
    this.setState({
      gameStarted: dataReceived["gameStarted"],
    });

    if (dataReceived["playerData"]) {
      this.setState({
        points: dataReceived["playerData"]["points"],
        position: dataReceived["playerData"]["position"],
        rank: dataReceived["playerData"]["rank"],
        userBadges: dataReceived["playerData"]["badges"],
      });
    }

    if (this.state.gameStarted) {
      this.sendJoinGameMessage();
    }
  }

  handleBadgesReceived(dataReceived) {
    this.setState({
      badges: dataReceived["badges"],
    });
  }

  sendJoinGameMessage() {
    const user = JSON.parse(sessionStorage.getItem("user"));

    const dataToSend = {
      type: "join game",
      board: "Histopolio", // TODO: ter no url
      userId: user.id,
      name: user.name,
      email: user.email,
      avatar: user.avatar,
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
      question: dataReceived["questionData"],
    });

    this.hideDice();
  }

  handleUpdate(dataReceived) {
    this.setState({
      points: dataReceived["points"],
      position: dataReceived["position"],
      rank: dataReceived["rank"],
    });
  }

  handleContentReceived(dataReceived) {
    this.setState({
      content: dataReceived["content"],
    });

    this.hideDice();
  }

  handleAnswer(answerIndex) {
    this.setState({ question: null });

    const answer = answerIndex + 1;

    const dataToSend = {
      type: "answer",
      answer: answer,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  handleContentClick() {
    this.setState({ content: "" });

    const dataToSend = {
      type: "content viewed",
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

  handleStoreClick() {
    this.setState({
      storeOpen: true,
    });
  }

  handleCloseStoreClick() {
    this.setState({
      storeOpen: false,
    });
  }

  handleBadgePurchased(badgeId, cost) {
    const newUserBadges = [...this.state.userBadges];
    newUserBadges.push(badgeId);
    const newPoints = this.state.points - cost;

    this.setState({
      points: newPoints,
      userBadges: newUserBadges,
    });

    const user = JSON.parse(sessionStorage.getItem("user"));

    const dataToSend = {
      type: "badge purchased",
      userId: user.id,
      board: "Histopolio", // TODO: usar url
      save: "Turma1", // TODO: usar url
      badgeId: badgeId,
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  render() {
    return (
      <div>
        <nav aria-label="breadcrumb" className="m-4">
          <ol className="breadcrumb">
            <li className="breadcrumb-item">
              <Link to="/">Home</Link>
            </li>
            <li className="breadcrumb-item active" aria-current="page">
              Histopolio
            </li>
          </ol>
        </nav>
        {this.state.gameStarted ? (
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
                {this.state.question ? (
                  <div>
                    <Question
                      question={this.state.question}
                      onAnswerClick={this.handleAnswer}
                    />
                  </div>
                ) : (
                  <div className="text-center">
                    {this.state.content.length > 0 ? (
                      <a
                        href={this.state.content}
                        target="_blank"
                        rel="noreferrer"
                      >
                        <button
                          className="btn btn-primary btn-lg mt-4"
                          onClick={this.handleContentClick}
                        >
                          Ver conteúdo
                        </button>
                      </a>
                    ) : (
                      <h2>Espera pela tua vez!</h2>
                    )}
                  </div>
                )}
              </div>
            )}
            <p>Rank: {this.state.rank}</p>
          </div>
        ) : (
          <div>
            {this.state.storeOpen ? (
              <Store
                points={this.state.points}
                badges={this.state.badges}
                userBadges={this.state.userBadges}
                onPurchaseClick={this.handleBadgePurchased}
                onCloseClick={this.handleCloseStoreClick}
              />
            ) : (
              <Wait
                points={this.state.points}
                rank={this.state.rank}
                onStoreClick={this.handleStoreClick}
              />
            )}
          </div>
        )}
      </div>
    );
  }
}

export default Play;
