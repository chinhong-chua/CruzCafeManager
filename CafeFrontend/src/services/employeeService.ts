import api from './api';
import { Employee } from '../interfaces';

export const getEmployees = (cafeName?: string) => {
  return api.get<Employee[]>('/employees', {
    params: { cafeName },
  });
};

export const getEmployeeById = (id: string) => {
  return api.get<Employee>(`/employees/${id}`);
};

export const createEmployee = (data: Employee) => {
  return api.post('/employees', data);
};

export const updateEmployee = (id: string, data: Employee) => {
  return api.put(`/employees/${id}`, data);
};

export const deleteEmployee = (id: string) => {
  return api.delete(`/employees/${id}`);
};
