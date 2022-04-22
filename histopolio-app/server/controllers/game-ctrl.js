async function sendQuestionToFrontend(frontendWS, dataReceived) {
    frontendWS[0].send(JSON.stringify(dataReceived))
}

async function sendAnswerToUnity(unityWS, dataReceived) {
    unityWS.send(JSON.stringify(dataReceived));
}

module.exports = {
    sendQuestionToFrontend,
    sendAnswerToUnity,
};