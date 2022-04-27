const { sendQuestionToFrontend, sendAnswerToUnity, sendGameStatusToFrontend } = require('./game-ctrl');
const { loadBoard, loadQuestions, loadCards } = require('./load-ctrl');
let unityWS = null;
let frontendWSs = [];

async function processMessage(ws, data) {
    console.log(data);

    const dataReceived = JSON.parse(data);
    const command = dataReceived['type'];

    switch (command) {
        case 'question':
            await sendQuestionToFrontend(frontendWSs, dataReceived);
            break;
        case 'identification':
            await authentication(ws, dataReceived);
            break;
        case 'answer':
            await sendAnswerToUnity(unityWS, dataReceived);
            break;
        case 'load board':
            await loadBoard(unityWS, dataReceived);
            break;
        case 'load questions':
            await loadQuestions(unityWS, dataReceived);
            break;
        case 'load cards':
            await loadCards(unityWS, dataReceived);
            break;
        case 'game status':
            await sendGameStatusToFrontend(ws, unityWS != null);
            break;
        default:
            console.log('Unknown message: ' + data);
    }
}

async function authentication(ws, dataReceived) {
    if (dataReceived['id'] == 'unity') {
        unityWS = ws;
        console.log('Game connected');

        frontendWSs.forEach(frontendWS => {
            sendGameStatusToFrontend(frontendWS, true);
        });
    }
    else {
        frontendWSs.push(ws);
        console.log('Users connected: ' + frontendWSs.length)
    }
}

module.exports = {
    processMessage,
};
