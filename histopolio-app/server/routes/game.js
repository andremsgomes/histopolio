const express = require("express");
const router = express.Router();
const gameController = require("../controllers/game-ctrl.js");

router.get("/data/:board", gameController.getSavedData);
router.get("/data/:board/:user_id", gameController.getPlayerSavedData);
router.post("/data/update", gameController.updateSavedData);

module.exports = router;