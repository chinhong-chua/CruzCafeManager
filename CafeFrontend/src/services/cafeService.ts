import api from './api';
import { Cafe } from '../interfaces';

export const getCafes = (location?: string) => {
  return api.get<Cafe[]>('/cafes', {
    params: { location },
  });
};

export const getCafeById = (id: string) => {
    return api.get<Cafe>(`/cafes/${id}`);
  };
  

  export const createCafe = (data: Cafe) => {
    return api.post('/cafes', data);
  };
  
  export const updateCafe = (id: string, data: Cafe) => {
    return api.put(`/cafes/${id}`, data);
  };

export const deleteCafe = (id: string) => {
  return api.delete(`/cafes/${id}`);
};
