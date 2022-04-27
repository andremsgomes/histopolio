async function sendQuestionToFrontend(frontendWS, dataReceived) {
    frontendWS[0].send(JSON.stringify(dataReceived))
}

async function sendAnswerToUnity(unityWS, dataReceived) {
    unityWS.send(JSON.stringify(dataReceived));
}

async function sendNewPlayerToUnity(unityWS, playerName) {
    const dataToSend = {
        type: "player",
        name: playerName
    };

    unityWS.send(JSON.stringify(dataToSend));
}

async function sendGameStatusToFrontend(frontendWS, gameStarted) {
    const dataToSend = {
        type: "game status",
        gameStarted: gameStarted
    };

    frontendWS.send(JSON.stringify(dataToSend));
}

module.exports = {
    sendQuestionToFrontend,
    sendAnswerToUnity,
    sendNewPlayerToUnity,
    sendGameStatusToFrontend
};