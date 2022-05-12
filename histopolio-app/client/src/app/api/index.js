import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:8080/",
});

export const login = (payload) => api.post("/api/auth/login", payload);
export const signup = (payload) => api.post("/api/auth/signup", payload);
export const saves = (board) => api.get(`/api/game/data/${board}/saves`);
export const boardData = (board) => api.get(`/api/game/data/${board}`);
export const savedData = (board, save) =>
  api.get(`/api/game/data/${board}/${save}`);
export const playerData = (board, save, userId) =>
  api.get(`/api/game/data/${board}/${save}/${userId}`);
export const updateSave = (payload) =>
  api.post("api/game/data/save/update", payload);
export const updateBoard = (payload) =>
  api.post("api/game/data/board/update", payload);

const apiRoutes = {
  login,
  signup,
  saves,
  boardData,
  savedData,
  playerData,
  updateSave,
  updateBoard,
};

export default apiRoutes;
