const express = require("express");
const router = express.Router();
const gameController = require("../controllers/game-ctrl.js");

router.get("/data/:board/:user_id", gameController.getSavedData);

module.exports = router;