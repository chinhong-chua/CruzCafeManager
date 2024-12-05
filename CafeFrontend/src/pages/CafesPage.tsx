import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Box, Button, TextField, Typography } from "@mui/material";
import { getCafes, deleteCafe } from "../services/cafeService";
import CafeTable from "../components/CafeTable";
import { Cafe } from "../interfaces";
import LoadingScreen from "../components/LoadingScreen";
import useDebounce from "../components/hooks/useDebounce";

const CafesPage: React.FC = () => {
  const [cafes, setCafes] = useState<Cafe[]>([]);
  const [locationFilter, setLocationFilter] = useState("");
  const debouncedLocationFilter = useDebounce(locationFilter, 600);

  const [loading, setLoading] = useState<boolean>(true);

  const navigate = useNavigate();

  // Debounce the locationFilter input
  // useEffect(() => {
  //   const handler = setTimeout(() => {
  //     setDebouncedLocationFilter(locationFilter);
  //   }, 500);

  //   // Cleanup timeout if locationFilter changes before delay is over
  //   return () => {
  //     clearTimeout(handler);
  //   };
  // }, [locationFilter]);

  // Fetch cafes whenever debouncedLocationFilter changes
  useEffect(() => {
    const fetchCafes = async () => {
      setLoading(true);
      try {
        const response = await getCafes(debouncedLocationFilter);
        setCafes(response.data);
      } catch (error) {
        console.error("Failed to fetch cafes", error);
      } finally {
        setLoading(false);
      }
    };
    fetchCafes();
  }, [debouncedLocationFilter]);

  const handleAddCafe = () => {
    navigate("/cafes/add");
  };

  const handleEditCafe = (id: string) => {
    navigate(`/cafes/edit/${id}`);
  };

  const handleDeleteCafe = async (id: string) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this cafe?"
    );
    if (confirmDelete) {
      try {
        await deleteCafe(id);
        // Re-fetch cafes after deletion
        const response = await getCafes(debouncedLocationFilter);
        setCafes(response.data);
      } catch (error) {
        console.error("Failed to delete cafe", error);
      }
    }
  };

  const handleEmployeesClick = (cafeName: string) => {
    if (!cafeName) return;
    navigate(`/employees?cafe=${encodeURIComponent(cafeName)}`);
  };

  if (loading) {
    return <LoadingScreen />;
  }

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
          style={{ marginRight: "16px" }}
        />
        <Button
          variant="contained"
          color="primary"
          onClick={handleAddCafe}
          style={{ marginRight: "16px" }}
        >
          Add New Cafe
        </Button>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => navigate("/employees")}
        >
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
