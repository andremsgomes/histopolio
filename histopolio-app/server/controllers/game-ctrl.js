async function sendQuestionToFrontend(frontendWS, dataReceived) {
    frontendWS.send(JSON.stringify(dataReceived))
}

async function sendAnswerToUnity(unityWS, dataReceived) {
    unityWS.send(JSON.stringify(dataReceived));
}

async function sendGameStatusToFrontend(frontendWS, gameStarted) {
    const dataToSend = {
        type: "game status",
        gameStarted: gameStarted
    };

    frontendWS.send(JSON.stringify(dataToSend));
}

async function sendNewPlayerToUnity(unityWS, dataReceived) {
    unityWS.send(JSON.stringify(dataReceived));
}

module.exports = {
    sendQuestionToFrontend,
    sendAnswerToUnity,
    sendNewPlayerToUnity,
    sendGameStatusToFrontend
};