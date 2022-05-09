const { readJSONFile, writeJSONFile } = require("./../utils/json-utils");

async function sendQuestionToFrontend(frontendWS, dataReceived) {
  frontendWS.send(JSON.stringify(dataReceived));
}

async function sendAnswerToUnity(unityWS, dataReceived) {
  unityWS.send(JSON.stringify(dataReceived));
}

async function sendGameStatusToFrontend(userId, frontendWS, gameStarted) {
  let playerData = await getPlayerData("Histopolio", userId);

  if (!playerData) {
    playerData = {
      points: 20,
      position: 0,
    };
  }

  const dataToSend = {
    type: "game status",
    gameStarted: gameStarted,
    playerData: playerData,
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

function newGame(userIds, dataReceived) {
  let savedData = [];

  for (const id of userIds) {
    const player = {
      userId: id,
      points: 20,
      position: 0,
    };

    savedData.push(player);
  }

  writeJSONFile("./data/" + dataReceived.board + "/SavedData.json", savedData); // TODO: allow multiple saves

  console.log("New Game Started!");
}

function saveGame(dataReceived) {
  const players = readJSONFile(
    "./data/" + dataReceived.board + "/SavedData.json"
  );

  const newSavedData = players.map((player) => {
    if (player.userId === dataReceived["userId"]) {
      player.points = dataReceived["points"];
      player.position = dataReceived["position"];
    }

    return player;
  });

  writeJSONFile(
    "./data/" + dataReceived.board + "/SavedData.json",
    newSavedData
  );

  console.log("Game saved!");
}

async function getPlayerData(board, userId) {
  const savedData = readJSONFile("./data/" + board + "/SavedData.json");

  return savedData.find((player) => player.userId == userId);
}

async function getSavedData(req, res) {
  const board = req.params.board;
  const userId = req.params.user_id;

  const player = await getPlayerData(board, userId);

  if (!player) {
    return res
      .status(404)
      .send({ error: true, message: "O utilizador nunca jogou" });
  }

  return res.status(200).json(player);
}

module.exports = {
  sendQuestionToFrontend,
  sendAnswerToUnity,
  sendNewPlayerToUnity,
  sendGameStatusToFrontend,
  sendTurnToFrontend,
  sendDiceResultToUnity,
  sendInfoShownToFrontend,
  newGame,
  saveGame,
  getSavedData,
};
