const express = require('express')

const ApiCtrl = require('../controllers/api-ctrl')

const router = express.Router()

router.get('/answer', ApiCtrl.getAnswer)

module.exports = router