const { sendQuestionToFrontend, sendAnswerToUnity } = require('./game-ctrl');
const { loadBoard, loadQuestions } = require('./load-ctrl');
let unityWS = null;
let frontendWSs = [];

async function processMessage(ws, data) {
    console.log(data);

    const dataReceived = JSON.parse(data);
    const command = dataReceived['type'];

    switch(command) {
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
        default:
            console.log('Unknown message: ' + data);
    }
}

async function authentication(ws, dataReceived) {
    if (dataReceived['id'] == 'unity')
        unityWS = ws
    else {
        frontendWSs.push(ws)
        console.log('Users connected: ' + frontendWSs.length)
    }
}

module.exports = {
    processMessage,
};
  