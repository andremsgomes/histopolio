let unityWS = null
let reactWS = []

async function processMessage(ws, data) {
    console.log(data);

    const dataReceived = JSON.parse(data);
    const command = dataReceived['type'];

    switch(command) {
        case 'question':
            await handleQuestionReceived(dataReceived);
            break;
        case 'identification':
            await handleIdentificationReceived(ws, dataReceived);
            break;
        case 'answer':
            await handleAnswerReceived(dataReceived);
            break;
        default:
            console.log('Unknown message: ' + data);
    }
}

async function handleQuestionReceived(dataReceived) {
    reactWS[0].send(JSON.stringify(dataReceived))
}

async function handleIdentificationReceived(ws, dataReceived) {
    if (dataReceived['id'] == 'unity')
        unityWS = ws
    else {
        reactWS.push(ws)
        console.log('Users connected: ' + reactWS.length)
    }
}

async function handleAnswerReceived(dataReceived) {
    unityWS.send(JSON.stringify(dataReceived));
}

module.exports = {
    processMessage,
};
  