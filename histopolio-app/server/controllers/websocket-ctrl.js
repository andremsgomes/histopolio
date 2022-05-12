const gameController = require("./game-ctrl");
const loadController = require("./load-ctrl");

let unityWS = null;
let frontendWSs = new Map();

async function processMessage(ws, data) {
  console.log(data);

  const dataReceived = JSON.parse(data);
  const command = dataReceived["type"];

  switch (command) {
    case "question":
      await gameController.sendQuestionToFrontend(
        frontendWSs.get(dataReceived["userId"]),
        dataReceived
      );
      break;
    case "identification":
      await authentication(ws, dataReceived);
      break;
    case "answer":
      await gameController.sendAnswerToUnity(unityWS, dataReceived);
      break;
    case "load board":
      await loadController.loadBoard(unityWS, dataReceived);
      break;
    case "load questions":
      await loadController.loadQuestions(unityWS, dataReceived);
      break;
    case "load cards":
      await loadController.loadCards(unityWS, dataReceived);
      break;
    case "game status":
      await gameController.sendGameStatusToFrontend(ws, dataReceived);
      break;
    case "join game":
      await gameController.addPlayerToGame(unityWS, dataReceived);
      break;
    case "turn":
      await gameController.sendTurnToFrontend(
        frontendWSs.get(dataReceived["userId"])
      );
      break;
    case "dice result":
      await gameController.sendDiceResultToUnity(unityWS, dataReceived);
      break;
    case "info shown":
      await gameController.sendInfoShownToFrontend(
        frontendWSs.get(dataReceived["userId"]),
        dataReceived
      );
      break;
    case "save":
      gameController.saveGame(dataReceived);
      break;
    case "load game":
      gameController.loadGame(frontendWSs, dataReceived);
      break;
    case "load saves":
      loadController.loadSaves(unityWS, dataReceived);
      break;
    default:
      console.log("Unknown message: " + data);
  }
}

async function authentication(ws, dataReceived) {
  if (dataReceived["platform"] == "unity") {
    unityWS = ws;
    console.log("Unity connected");
  } else {
    frontendWSs.set(dataReceived["id"], ws);
    console.log("Users connected: " + frontendWSs.size);
  }
}

module.exports = {
  processMessage,
};
