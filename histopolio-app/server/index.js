const express = require('express')
const bodyParser = require('body-parser')
const http = require('http');

const WebSocket = require('ws');

const app = express()
const apiPort = 8080

const server = http.createServer(app);
const wss = new WebSocket.Server({ server: server }, ()=>{
    console.log('server started')
});

wss.on('connection', function connection(ws) {
    console.log('A new client connected!')

    ws.on('message', function message(data) {
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
    });
});

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

app.get('/', (req, res) => {
    res.send('Hello World!')
})

server.listen(apiPort, () => console.log(`Server running on port ${apiPort}`))