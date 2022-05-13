const express = require("express");
const router = express.Router();
const gameController = require("../controllers/game-ctrl.js");

router.get("/data/:board", gameController.getBoardData);
router.post("/data/board/update", gameController.updateBoardData);

router.get("/data/:board/:tile/questions", gameController.getQuestionsData);
router.post("/data/questions/new", gameController.newQuestion);

router.get("/data/:board/saves", gameController.getSaves);

router.get("/data/:board/:save", gameController.getSavedData);
router.post("/data/save/update", gameController.updateSavedData);

router.get("/data/:board/:save/:user_id", gameController.getPlayerSavedData);

module.exports = router;
