const express = require('express')
const bodyParser = require('body-parser')
const http = require('http');

const WebSocket = require('ws');

const apiRouter = require('./routes/api-router')
// const db = require('./db')

const app = express()
const apiPort = 8080

const server = http.createServer(app);
const wss = new WebSocket.Server({ server: server }, ()=>{
    console.log('server started')
});

wss.on('connection', function connection(ws) {
    console.log('A new client connected!')
    ws.send('Welcome New Client')

    ws.on('message', function message(data) {
        console.log('received: %s', data);
    });
});

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

// db.on('error', console.error.bind(console, 'MongoDB connection error:'))

app.get('/', (req, res) => {
    res.send('Hello World!')
})

app.use('/api', apiRouter)

server.listen(apiPort, () => console.log(`Server running on port ${apiPort}`))