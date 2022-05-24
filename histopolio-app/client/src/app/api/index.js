import axios from "axios";

const api = axios.create({
  baseURL: "https://histopolio-app-backend.herokuapp.com/",
});

export const login = (payload) => api.post("/api/auth/login", payload);
export const signup = (payload) => api.post("/api/auth/signup", payload);
export const saves = (board) => api.get(`/api/game/data/${board}/saves`);
export const boardData = (board) => api.get(`/api/game/data/${board}`);
export const questionsData = (board, tile) =>
  api.get(`/api/game/data/${board}/${tile}/questions`);
export const savedData = (board, save) =>
  api.get(`/api/game/data/${board}/saves/${save}`);
export const playerData = (board, userId) =>
  api.get(`/api/game/data/${board}/${userId}/player`);
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
export const badgesData = (board) => api.get(`/api/game/data/${board}/badges`);
export const newBadge = (payload) =>
  api.post("/api/game/data/badges/new", payload);

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
  badgesData,
  newBadge,
};

export default apiRoutes;
