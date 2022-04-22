import './App.css';
import { Component } from 'react';
import { w3cwebsocket } from 'websocket';

import Play from './app/play/Play';

const client = new w3cwebsocket('ws://localhost:8080');

class App extends Component {
  constructor() {
      super();

      this.handleAnswer = this.handleAnswer.bind(this);
  }

  state = {
    showQuestion: false,
    question: "",
    answers: []
  };
  
  componentDidMount() {
    client.onopen = () => {
      console.log('WebSocket Client Connected')

      this.sendIdentificationMessage()
    }

    client.onmessage = (message) => {
      console.log(message.data)
      const dataReceived = JSON.parse(message.data)

      this.processDataReceived(dataReceived)
    }
  }

  sendIdentificationMessage() {
    const dataToSend = {
      type: 'identification',
      id: 'react'
    }

    this.sendToServer(JSON.stringify(dataToSend))
  }

  sendToServer(message) {
    client.send(message)
  }

  processDataReceived(dataReceived) {
    const command = dataReceived['type'];

    switch(command) {
      case 'question':
        this.handleQuestionReceived(dataReceived)
        break;
      default:
        console.log('Unknown message: ' + dataReceived);
    }
  }

  handleQuestionReceived(dataReceived) {
    this.setState({
      question: dataReceived['question'],
      answers: dataReceived['answers'],
      showQuestion: true
    });
  }

  handleAnswer(answerIndex) {
    this.setState({ showQuestion: false });

    const answer = answerIndex + 1;

    const dataToSend = {
      type: 'answer',
      answer: answer
    };

    this.sendToServer(JSON.stringify(dataToSend));
  }

  render() {
    return (
      <div className="App">
        <Play showQuestion={this.state.showQuestion} question={this.state.question} answers={this.state.answers} onAnswer={this.handleAnswer} />
      </div>
    );
  }
}

export default App;
