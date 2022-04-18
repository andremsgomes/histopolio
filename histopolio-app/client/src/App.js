import logo from './logo.svg';
import './App.css';
import { Component } from 'react';
import { w3cwebsocket } from 'websocket';

const client = new w3cwebsocket('ws://localhost:8080');

class App extends Component {
  componentDidMount() {
    client.onopen = () => {
      console.log('WebSocket Client Connected');
    }
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
        </header>
      </div>
    );
  }
}

export default App;
