const { readJSONFile, writeJSONFile } = require('./../utils/json-utils');

async function loadBoard(ws, dataReceived) {
    const board = readJSONFile("./data/" + dataReceived.board + ".json");

    const dataToSend = {
        type: "board",
        board: board
    }

    ws.send(JSON.stringify(dataToSend));
}

module.exports = {
    loadBoard,
};