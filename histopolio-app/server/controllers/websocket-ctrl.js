let unityWS = null
let reactWS = []

async function processMessage(ws, data) {
    console.log(data);

    const dataReceived = JSON.parse(data);
    const command = dataReceived['type'];

    switch(command) {
        case 'question':
            const dataToSend = {
                type: 'answer',
                answer: 2
            }

            ws.send(JSON.stringify(dataToSend))
            break
        case 'identification':
            if (dataReceived['id'] == 'unity')
                unityWS = ws;
            else {
                reactWS.push(ws);
                console.log('Users connected: ' + reactWS.length)
            }
            break;
        default:
            console.log('Unknown message: ' + data)
    }
}

module.exports = {
    processMessage,
  };
  