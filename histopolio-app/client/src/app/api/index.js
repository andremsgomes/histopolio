import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:8080/",
});

export const login = (payload) => api.post("/api/auth/login", payload);
export const signup = (payload) => api.post("/api/auth/signup", payload);
export const savedData = (board) => api.get(`/api/game/data/${board}`);
export const playerData = (board, userId) =>
  api.get(`/api/game/data/${board}/${userId}`);
export const updateData = (payload) =>
  api.post("api/game/data/update", payload);

const apiRoutes = {
  login,
  savedData,
  signup,
  playerData,
  updateData,
};

export default apiRoutes;
