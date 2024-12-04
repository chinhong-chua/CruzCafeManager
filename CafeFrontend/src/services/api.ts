import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:8011/'
});

export default api;
