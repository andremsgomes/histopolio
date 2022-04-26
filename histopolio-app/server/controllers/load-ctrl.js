const { readJSONFile, writeJSONFile } = require('./../utils/json-utils');

async function loadBoard(ws, dataReceived) {
    const board = readJSONFile("./data/" + dataReceived.board + "/BoardData.json");

    const dataToSend = {
        type: "board",
        board: board
    }

    ws.send(JSON.stringify(dataToSend));
}

async function loadQuestions(ws, dataReceived) {
    const questions = readJSONFile("./data/" + dataReceived.board + "/Questions.json");

    const dataToSend = {
        type: "questions",
        questions: questions
    }

    ws.send(JSON.stringify(dataToSend));
}

module.exports = {
    loadBoard,
    loadQuestions
};