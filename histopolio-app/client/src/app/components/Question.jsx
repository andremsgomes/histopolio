import React from "react";

function Question(props) {
  return (
    <div className="row m-4">
      <div className="col-sm-12 col-md-8 col-lg-6 mx-auto text-center mb-4">
        <h1>{props.question.question}</h1>
        {props.question.image.length > 0 && (
          <img
            src={props.question.image}
            className="rounded mb-4"
            alt="question"
            width="100%"
          />
        )}
        {props.question.answers.map((answer, index) => (
          <button
            className="btn btn-primary btn-lg mt-2"
            style={{ height: "100px", width: "100%" }}
            onClick={() => {
              props.onAnswerClick(index);
            }}
          >
            {answer}
          </button>
        ))}
      </div>
    </div>
  );
}

export default Question;
