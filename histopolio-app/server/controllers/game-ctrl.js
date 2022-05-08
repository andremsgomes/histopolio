const { readJSONFile, writeJSONFile } = require("./../utils/json-utils");

async function sendQuestionToFrontend(frontendWS, dataReceived) {
  frontendWS.send(JSON.stringify(dataReceived));
}

async function sendAnswerToUnity(unityWS, dataReceived) {
  unityWS.send(JSON.stringify(dataReceived));
}

async function sendGameStatusToFrontend(frontendWS, gameStarted) {
  const dataToSend = {
    type: "game status",
    gameStarted: gameStarted,
  };

  frontendWS.send(JSON.stringify(dataToSend));
}

async function sendNewPlayerToUnity(unityWS, dataReceived) {
  unityWS.send(JSON.stringify(dataReceived));
}

async function sendTurnToFrontend(frontendWS) {
  const dataToSend = {
    type: "turn",
  };

  frontendWS.send(JSON.stringify(dataToSend));
}

async function sendDiceResultToUnity(unityWS, dataReceived) {
  // wait for dice to stop rolling
  await new Promise((resolve) => setTimeout(resolve, dataReceived["rollTime"]));

  unityWS.send(JSON.stringify(dataReceived));
}

async function sendInfoShownToFrontend(frontendWS, dataReceived) {
  frontendWS.send(JSON.stringify(dataReceived));
}

function saveGame(dataReceived) {
  console.log(dataReceived);

  const users = readJSONFile("./data/Users.json");

  const newUsers = users.map((user) => {
    if (user.id === dataReceived["userId"]) {
      user.game.points = dataReceived["points"];
      user.game.position = dataReceived["position"];
    }

    return user;
  });

  console.log(newUsers);

  writeJSONFile("./data/Users.json", newUsers);
}

module.exports = {
  sendQuestionToFrontend,
  sendAnswerToUnity,
  sendNewPlayerToUnity,
  sendGameStatusToFrontend,
  sendTurnToFrontend,
  sendDiceResultToUnity,
  sendInfoShownToFrontend,
  saveGame,
};
