const { readJSONFile, writeJSONFile } = require("./../utils/json-utils");

let gameSaveFilePath = "";

async function sendQuestionToFrontend(frontendWS, dataReceived) {
  frontendWS.send(JSON.stringify(dataReceived));
}

async function sendAnswerToUnity(unityWS, dataReceived) {
  unityWS.send(JSON.stringify(dataReceived));
}

async function sendGameStatusToFrontend(userId, frontendWS, saveFile) {
  let playerData = await getPlayerData(saveFile, userId);

  if (!playerData) {
    playerData = {
      points: 20,
      position: 0,
    };
  }

  const dataToSend = {
    type: "game status",
    gameStarted: gameSaveFilePath.length > 0,
    playerData: playerData,
  };

  frontendWS.send(JSON.stringify(dataToSend));
}

function addPlayerToGame(unityWS, dataReceived) {
  let player = getPlayerData(gameSaveFilePath, dataReceived["userId"]);

  if (!player) {
    player = {
      userId: dataReceived["userId"],
      points: 20,
      position: 0
    }

    let players = readJSONFile(gameSaveFilePath);
    players.push(player);
    writeJSONFile(gameSaveFilePath, players);
  }

  const dataToSend = {
    type: "join game",
    userId: player.userId,
    name: dataReceived["name"],
    points: player.points,
    position: player.position
  }

  unityWS.send(JSON.stringify(dataToSend));
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

function newGame(frontendWSs, dataReceived) {
  gameSaveFilePath = "./data/" + dataReceived.board + "/SavedData.json";

  writeJSONFile(gameSaveFilePath, []); // TODO: allow multiple saves

  console.log("New Game Started!");

  for (id of frontendWSs.keys()) {
    sendGameStatusToFrontend(id, frontendWSs.get(id), gameSaveFilePath);
  }
}

async function loadGame(frontendWSs, dataReceived) {
  gameSaveFilePath = "./data/" + dataReceived.board + "/SavedData.json";

  console.log("Game Loaded!");

  for (id of frontendWSs.keys()) {
    sendGameStatusToFrontend(id, frontendWSs.get(id), gameSaveFilePath);
  }
}

function saveGame(dataReceived) {
  const players = readJSONFile(gameSaveFilePath);

  const newSavedData = players.map((player) => {
    if (player.userId === dataReceived["userId"]) {
      player.points = dataReceived["points"];
      player.position = dataReceived["position"];
    }

    return player;
  });

  writeJSONFile(gameSaveFilePath, newSavedData);

  console.log("Game Saved!");
}

function getPlayerData(file, userId) {
  const savedData = readJSONFile(file);

  return savedData.find((player) => player.userId == userId);
}

async function getSavedData(req, res) {
  const board = req.params.board;
  const userId = req.params.user_id;

  const player = getPlayerData("./data/" + board + "/SavedData.json", userId);

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
  addPlayerToGame,
  sendGameStatusToFrontend,
  sendTurnToFrontend,
  sendDiceResultToUnity,
  sendInfoShownToFrontend,
  newGame,
  loadGame,
  saveGame,
  getSavedData,
};
