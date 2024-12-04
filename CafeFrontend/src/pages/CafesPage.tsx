import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Box, Button, TextField, Typography } from '@mui/material';
import { getCafes, deleteCafe } from '../services/cafeService';
import CafeTable from '../components/CafeTable';
import { Cafe } from '../interfaces';

const CafesPage: React.FC = () => {
  const [cafes, setCafes] = useState<Cafe[]>([]);
  const [locationFilter, setLocationFilter] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchCafes();
  }, [locationFilter]);

  const fetchCafes = async () => {
    try {
      const response = await getCafes(locationFilter);
      setCafes(response.data);
    } catch (error) {
      console.error('Failed to fetch cafes', error);
    }
  };

  const handleAddCafe = () => {
    navigate('/cafes/add');
  };

  const handleEditCafe = (id: string) => {
    navigate(`/cafes/edit/${id}`);
  };

  const handleDeleteCafe = async (id: string) => {
    const confirmDelete = window.confirm('Are you sure you want to delete this cafe?');
    if (confirmDelete) {
      try {
        await deleteCafe(id);
        fetchCafes();
      } catch (error) {
        console.error('Failed to delete cafe', error);
      }
    }
  };

  const handleEmployeesClick = (cafeName: string) => {
    if(!cafeName) return;
    navigate(`/employees?cafe=${encodeURIComponent(cafeName)}`);
  };

  return (
    <Box padding={2}>
      <Typography variant="h4" gutterBottom>
        Cafes
      </Typography>
      <Box display="flex" alignItems="center" marginBottom={2}>
        <TextField
          label="Filter by Location"
          value={locationFilter}
          onChange={(e) => setLocationFilter(e.target.value)}
          style={{ marginRight: '16px' }}
        />
        <Button variant="contained" color="primary" onClick={handleAddCafe} style={{ marginRight: '16px' }}>
          Add New cafe
        </Button>
        <Button variant="contained" color="secondary" onClick={()=> navigate('/employees')}>
          Go to Employees
        </Button>
      </Box>
      <CafeTable
        data={cafes}
        onEdit={handleEditCafe}
        onDelete={handleDeleteCafe}
        onEmployeesClick={handleEmployeesClick}
      />
    </Box>
  );
};

export default CafesPage;
