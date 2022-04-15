const mongoose = require('mongoose')

mongoose
    .connect('mongodb://mongo:27017/sinkeo', { useNewUrlParser: true, useUnifiedTopology: true })
    .catch(e => {
        console.error('Connection error', e.message)
    })

const db = mongoose.connection

module.exports = db