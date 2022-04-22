import React, { Component } from 'react';

class Play extends Component {

  constructor(props) {
    super(props)
  }
  
  render() {
    return (
      <div>
        {this.props.showQuestion ? (
          <div>
            <p>{this.props.question}</p>
            {this.props.answers.map((answer, index) => <button onClick={() => {this.props.onAnswer(index)}}>{answer}</button>)}
          </div>
        ) : (
          <p>Wait for your turn</p>
        )}
      </div>
    );
  }
}
export default Play;