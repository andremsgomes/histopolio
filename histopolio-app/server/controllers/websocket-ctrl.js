const gameController = require('./game-ctrl');
const loadController = require('./load-ctrl');
let unityWS = null;
let frontendWSs = [];

async function processMessage(ws, data) {
    console.log(data);

    const dataReceived = JSON.parse(data);
    const command = dataReceived['type'];

    switch (command) {
        case 'question':
            await gameController.sendQuestionToFrontend(frontendWSs, dataReceived);
            break;
        case 'identification':
            await authentication(ws, dataReceived);
            break;
        case 'answer':
            await gameController.sendAnswerToUnity(unityWS, dataReceived);
            break;
        case 'load board':
            await loadController.loadBoard(unityWS, dataReceived);
            break;
        case 'load questions':
            await loadController.loadQuestions(unityWS, dataReceived);
            break;
        case 'load cards':
            await loadController.loadCards(unityWS, dataReceived);
            break;
        case 'game status':
            await gameController.sendGameStatusToFrontend(ws, unityWS != null);
            break;
        case 'join game':
            await gameController.sendNewPlayerToUnity(unityWS, dataReceived);
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
            gameController.sendGameStatusToFrontend(frontendWS, true);
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
