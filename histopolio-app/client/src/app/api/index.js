import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:8080/",
});

export const login = (payload) => api.post("/api/auth/login", payload);
export const signup = (payload) => api.post("/api/auth/signup", payload);
export const saves = (board) => api.get(`/api/game/data/${board}/saves`);
export const boardData = (board) => api.get(`/api/game/data/${board}`);
export const questionsData = (board, tile) =>
  api.get(`/api/game/data/${board}/${tile}/questions`);
export const savedData = (board, save) =>
  api.get(`/api/game/data/${board}/saves/${save}`);
export const playerData = (board, save, userId) =>
  api.get(`/api/game/data/${board}/${save}/${userId}/player`);
export const updateSave = (payload) =>
  api.post("api/game/data/save/update", payload);
export const updateBoard = (payload) =>
  api.post("api/game/data/board/update", payload);
export const newQuestion = (payload) =>
  api.post("api/game/data/questions/new", payload);
export const newDeckCard = (payload) =>
  api.post("api/game/data/cards/deck/new", payload);
export const trainCardsData = (board, tile) =>
  api.get(`/api/game/data/${board}/${tile}/train_cards`);
export const newTrainCard = (payload) =>
  api.post("api/game/data/cards/train_cards/new", payload);

const apiRoutes = {
  login,
  signup,
  saves,
  boardData,
  questionsData,
  savedData,
  playerData,
  updateSave,
  updateBoard,
  newQuestion,
  newDeckCard,
  trainCardsData,
  newTrainCard,
};

export default apiRoutes;
