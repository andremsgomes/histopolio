const express = require("express");
const router = express.Router();
const gameController = require("../controllers/game-ctrl.js");

router.get("/data/:board/saves", gameController.getSaves);
router.get("/data/:board/:save", gameController.getSavedData);
router.get("/data/:board/:save/:user_id", gameController.getPlayerSavedData);
router.post("/data/update", gameController.updateSavedData);

module.exports = router;