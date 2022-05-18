const WebSocket = require("ws");

const {
  readJSONFile,
  writeJSONFile,
  fileExists,
  getFilesFromDir,
} = require("../utils/file-utils");

let gameSaveFilePath = "";
let gameStarted = false;

async function sendQuestionToFrontend(frontendWS, dataReceived) {
  if (frontendWS != null && frontendWS.readyState === WebSocket.OPEN) {
    frontendWS.send(JSON.stringify(dataReceived));
  }
}

async function sendAnswerToUnity(unityWS, dataReceived) {
  if (unityWS != null && unityWS.readyState === WebSocket.OPEN) {
    unityWS.send(JSON.stringify(dataReceived));
  }
}

async function sendGameStatusToFrontend(frontendWS, userId, saveFilePath) {
  let playerData = await getPlayerData(saveFilePath, userId);

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

  if (frontendWS != null && frontendWS.readyState === WebSocket.OPEN) {
    frontendWS.send(JSON.stringify(dataToSend));
  }
}

async function setGameReady(frontendWSs) {
  gameStarted = true;

  for (id of frontendWSs.keys()) {
    sendGameStatusToFrontend(frontendWSs.get(id), id, gameSaveFilePath);
  }
}

async function sendEndGameToFrontend(frontendWSs) {
  gameSaveFilePath = "";
  gameStarted = false;

  const dataToSend = {
    type: "game status",
    gameStarted: false,
  };

  for (ws of frontendWSs.values()) {
    if (ws.readyState === WebSocket.OPEN) ws.send(JSON.stringify(dataToSend));
  }
}

function addPlayerToGame(unityWS, dataReceived) {
  let player = getPlayerData(gameSaveFilePath, dataReceived["userId"]);

  if (!player) {
    player = {
      userId: dataReceived["userId"],
      name: dataReceived["name"],
      email: dataReceived["email"],
      points: 20,
      position: 0,
      numTurns: 0,
    };

    let players = readJSONFile(gameSaveFilePath);
    players.push(player);
    writeJSONFile(gameSaveFilePath, players);
  }

  const dataToSend = {
    type: "join game",
    userId: player.userId,
    name: player.name,
    avatar: dataReceived["avatar"],
    points: player.points,
    position: player.position,
    numTurns: player.numTurns,
  };

  if (unityWS != null && unityWS.readyState === WebSocket.OPEN) {
    unityWS.send(JSON.stringify(dataToSend));
  }
}

async function removePlayerFromGame(unityWS, userId) {
  const dataToSend = {
    type: "remove player",
    userId: userId,
  };

  if (unityWS != null && unityWS.readyState === WebSocket.OPEN) {
    unityWS.send(JSON.stringify(dataToSend));
  }
}

async function sendTurnToFrontend(frontendWS) {
  const dataToSend = {
    type: "turn",
  };

  if (frontendWS != null && frontendWS.readyState === WebSocket.OPEN) {
    frontendWS.send(JSON.stringify(dataToSend));
  }
}

async function sendDiceResultToUnity(unityWS, dataReceived) {
  // wait for dice to stop rolling
  await new Promise((resolve) => setTimeout(resolve, dataReceived["rollTime"]));

  if (unityWS != null && unityWS.readyState === WebSocket.OPEN) {
    unityWS.send(JSON.stringify(dataReceived));
  }
}

async function sendInfoShownToFrontend(frontendWS, dataReceived) {
  if (frontendWS != null && frontendWS.readyState === WebSocket.OPEN) {
    frontendWS.send(JSON.stringify(dataReceived));
  }
}

async function loadGame(unityWS, dataReceived) {
  gameSaveFilePath = `./data/${dataReceived["board"]}/saves/${dataReceived["file"]}`;

  if (!fileExists(gameSaveFilePath)) {
    writeJSONFile(gameSaveFilePath, []);
    console.log("New Game Started!");
  } else console.log("Game Loaded!");

  const players = readJSONFile(gameSaveFilePath);
  const users = readJSONFile("./data/Users.json");

  players.forEach((player) => {
    player["avatar"] = users.find((user) => user.id === player.userId).avatar;
  });

  const dataToSend = {
    type: "players",
    players: players,
  };

  unityWS.send(JSON.stringify(dataToSend));
}

function saveGame(frontendWSs, dataReceived) {
  const players = readJSONFile(gameSaveFilePath);

  const newSavedData = players.map((player) => {
    if (player.userId === dataReceived["userId"]) {
      player.points = dataReceived["points"];
      player.position = dataReceived["position"];
      player.numTurns = dataReceived["numTurns"];
    }

    return player;
  });

  writeJSONFile(gameSaveFilePath, newSavedData);

  console.log("Game Saved!");

  sendUpdateToFrontend(frontendWSs);
}

async function sendUpdateToFrontend(frontendWSs) {
  const players = readJSONFile(gameSaveFilePath);

  players.sort((a, b) => b.points - a.points);

  let rank = 1;

  players.forEach((player) => {
    const dataToSend = {
      type: "update",
      points: player.points,
      position: player.position,
      rank: rank++,
    };

    ws = frontendWSs.get(player.userId);

    if (ws && ws.readyState === WebSocket.OPEN)
      ws.send(JSON.stringify(dataToSend));
  });
}

async function sendContentToFrontend(frontendWS, dataReceived) {
  if (frontendWS != null && frontendWS.readyState === WebSocket.OPEN) {
    frontendWS.send(JSON.stringify(dataReceived));
  }
}

async function sendContentViewedToUnity(unityWS, dataReceived) {
  if (unityWS != null && unityWS.readyState === WebSocket.OPEN) {
    unityWS.send(JSON.stringify(dataReceived));
  }
}

function getPlayerData(file, userId) {
  const savedData = readJSONFile(file);

  return savedData.find((player) => player.userId == userId);
}

async function getPlayerSavedData(req, res) {
  const board = req.params.board;
  const save = req.params.save;
  const userId = req.params.user_id;

  const player = getPlayerData(`./data/${board}/saves/${save}.json`, userId);

  if (!player) {
    return res
      .status(404)
      .send({ error: true, message: "O utilizador nunca jogou" });
  }

  return res.status(200).json(player);
}

async function getSavedData(req, res) {
  const board = req.params.board;
  const save = req.params.save;

  const savedData = readJSONFile(`./data/${board}/saves/${save}.json`);

  if (!savedData) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  return res.status(200).json(savedData);
}

function updateSavedData(req, res) {
  const { board, save, savedData } = req.body;

  writeJSONFile(`./data/${board}/saves/${save}.json`, savedData);

  return res.status(200).send();
}

async function getSaves(req, res) {
  const board = req.params.board;

  const saveFiles = getFilesFromDir(`./data/${board}/saves/`);

  return res.status(200).json(saveFiles);
}

async function getBoardData(req, res) {
  const board = req.params.board;

  let boardData = readJSONFile(`./data/${board}/BoardData.json`);

  if (!boardData) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  const questions = readJSONFile(`./data/${board}/Questions.json`);

  if (!questions) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro de perguntas não existe" });
  }

  boardData["groupPropertyTiles"].forEach((tile) => {
    tile["questions"] = 0;
  });

  boardData["payTiles"].forEach((tile) => {
    tile["questions"] = 0;
  });

  questions["questions"].forEach((question) => {
    boardData["groupPropertyTiles"].forEach((tile) => {
      if (question["tileId"] === tile["id"]) {
        tile["questions"]++;
      }
    });

    boardData["payTiles"].forEach((tile) => {
      if (question["tileId"] === tile["id"]) {
        tile["questions"]++;
      }
    });
  });

  const cards = readJSONFile(`./data/${board}/Cards.json`);

  if (!cards) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro de cartas não existe" });
  }

  boardData["stationTiles"].forEach((tile) => {
    tile["cards"] = 0;
  });

  cards["trainCards"].forEach((card) => {
    boardData["stationTiles"].forEach((tile) => {
      if (card["tileId"] === tile["id"]) {
        tile["cards"]++;
      }
    });
  });

  boardData["communityCards"] = cards["communityCards"];

  return res.status(200).json(boardData);
}

function updateBoardData(req, res) {
  const { boardData } = req.body;

  writeJSONFile(`./data/${boardData["name"]}/BoardData.json`, boardData);

  return res.status(200).send();
}

async function getQuestionsData(req, res) {
  const board = req.params.board;
  const tileId = parseInt(req.params.tile);

  const questions = readJSONFile(`./data/${board}/Questions.json`);

  if (!questions) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  let tileQuestions = [];

  questions["questions"].forEach((question) => {
    if (question["tileId"] === tileId) {
      tileQuestions.push(question);
    }
  });

  return res.status(200).json(tileQuestions);
}

function newQuestion(req, res) {
  const { board, tileId, question, image, answers, correctAnswer } = req.body;

  const questions = readJSONFile(`./data/${board}/Questions.json`);

  if (!questions) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  const lastId =
    questions["questions"].length > 0
      ? questions["questions"][questions["questions"].length - 1].id
      : 0;

  const newQuestion = {
    id: lastId + 1,
    tileId: tileId,
    question: question,
    image: image,
    answers: answers,
    correctAnswer: correctAnswer,
  };

  questions["questions"].push(newQuestion);

  writeJSONFile(`./data/${board}/Questions.json`, questions);

  return res.status(200).send();
}

function newCommunityCard(req, res) {
  const { board, info, points } = req.body;

  const cards = readJSONFile(`./data/${board}/Cards.json`);

  if (!cards) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  const lastId =
    cards["communityCards"].length > 0
      ? cards["communityCards"][cards["communityCards"].length - 1].id
      : 0;

  const newCommunityCard = {
    id: lastId + 1,
    info: info,
    points: points,
  };

  cards["communityCards"].push(newCommunityCard);

  writeJSONFile(`./data/${board}/Cards.json`, cards);

  return res.status(200).send();
}

async function getTrainCardsData(req, res) {
  const board = req.params.board;
  const tileId = parseInt(req.params.tile);

  const cards = readJSONFile(`./data/${board}/Cards.json`);

  if (!cards) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  let tileCards = [];

  cards["trainCards"].forEach((card) => {
    if (card["tileId"] === tileId) {
      tileCards.push(card);
    }
  });

  return res.status(200).json(tileCards);
}

function newTrainCard(req, res) {
  const { board, tileId, info, content } = req.body;

  const cards = readJSONFile(`./data/${board}/Cards.json`);

  if (!cards) {
    return res
      .status(404)
      .send({ error: true, message: "O ficheiro não existe" });
  }

  const lastId =
    cards["trainCards"].length > 0
      ? cards["trainCards"][cards["trainCards"].length - 1].id
      : 0;

  const newTrainCard = {
    id: lastId + 1,
    tileId: tileId,
    info: info,
    content: content,
  };

  cards["trainCards"].push(newTrainCard);

  writeJSONFile(`./data/${board}/Cards.json`, cards);

  return res.status(200).send();
}

module.exports = {
  sendQuestionToFrontend,
  sendAnswerToUnity,
  addPlayerToGame,
  removePlayerFromGame,
  sendGameStatusToFrontend,
  setGameReady,
  sendEndGameToFrontend,
  sendTurnToFrontend,
  sendDiceResultToUnity,
  sendInfoShownToFrontend,
  loadGame,
  saveGame,
  sendContentToFrontend,
  sendContentViewedToUnity,
  getPlayerSavedData,
  getSavedData,
  updateSavedData,
  getSaves,
  getBoardData,
  updateBoardData,
  getQuestionsData,
  newQuestion,
  newCommunityCard,
  getTrainCardsData,
  newTrainCard,
};
