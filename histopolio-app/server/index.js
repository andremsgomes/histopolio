const express = require('express')
const bodyParser = require('body-parser')

const apiRouter = require('./routes/api-router')
const db = require('./db')

const app = express()
const apiPort = 5000

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

db.on('error', console.error.bind(console, 'MongoDB connection error:'))

app.get('/', (req, res) => {
    res.send('Hello World!')
})

app.use('/api', apiRouter)

app.listen(apiPort, () => console.log(`Server running on port ${apiPort}`))