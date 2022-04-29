import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:8080/'
});

export const login = payload => api.post('/api/auth/login', payload)

const apiRoutes = {
    login
}

export default apiRoutes;