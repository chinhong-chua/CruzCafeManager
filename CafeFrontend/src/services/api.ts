import axios from 'axios';

const api = axios.create({
  baseURL: 'https://cafebackendapi.azure-api.net/' // Currently using apim endpoint, please update to your local api url
});

export default api;
