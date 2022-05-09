import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:8080/",
});

export const login = (payload) => api.post("/api/auth/login", payload);
export const signup = (payload) => api.post("/api/auth/signup", payload);
export const gameData = (board, userId) => api.get(`/api/game/data/${board}/${userId}`);

const apiRoutes = {
  login,
  signup,
  gameData,
};

export default apiRoutes;
