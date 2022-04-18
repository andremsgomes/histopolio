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
        default:
            console.log('Unknown message: ' + data)
    }
}

module.exports = {
    processMessage,
  };
  