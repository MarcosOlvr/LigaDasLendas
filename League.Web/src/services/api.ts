import axios, { AxiosInstance } from 'axios';

const api: AxiosInstance = axios.create({
    baseURL: "https://localhost:7251/api",
});

export default api;