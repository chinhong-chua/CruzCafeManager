import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import {
  Box,
  Button,
  MenuItem,
  Select,
  Typography,
  SelectChangeEvent,
  CircularProgress,
} from "@mui/material";
import { getEmployees, deleteEmployee } from "../services/employeeService";
import { getCafes } from "../services/cafeService";
import { Employee, Cafe } from "../interfaces";
import EmployeeTable from "../components/EmployeeTable";
import LoadingScreen from "../components/LoadingScreen";

const EmployeesPage: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const [cafeFilter, setCafeFilter] = useState<string>(() => {
    const params = new URLSearchParams(location.search);
    return params.get("cafe") || "";
  });

  const [employees, setEmployees] = useState<Employee[]>([]);
  const [cafes, setCafes] = useState<Cafe[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchCafesData = async () => {
      try {
        const cafesResponse = await getCafes();
        setCafes(cafesResponse.data);
      } catch (error) {
        console.error("Failed to fetch cafes", error);
      }
    };

    fetchCafesData();
  }, []);

  // Fetch employees whenever cafeFilter changes
  useEffect(() => {
    const fetchEmployees = async () => {
      setLoading(true);
      try {
        const response = await getEmployees(cafeFilter);
        setEmployees(response.data);
      } catch (error) {
        console.error("Failed to fetch employees", error);
      } finally {
        setLoading(false);
      }
    };

    fetchEmployees();
  }, [cafeFilter]);

  // Update cafeFilter when URL changes
  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const cafeQueryParam = params.get("cafe") || "";
    if (cafeFilter !== cafeQueryParam) {
      setCafeFilter(cafeQueryParam);
    }
  }, [location.search]);

  const handleAddEmployee = () => {
    navigate("/employees/add");
  };

  const handleEditEmployee = (id: string) => {
    navigate(`/employees/edit/${id}`);
  };

  const handleDeleteEmployee = async (id: string) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this employee?"
    );
    if (confirmDelete) {
      try {
        await deleteEmployee(id);
        const response = await getEmployees(cafeFilter);
        setEmployees(response.data);
      } catch (error) {
        console.error("Failed to delete employee", error);
      }
    }
  };

  const handleCafeClick = (cafeName: string) => {
    navigate(`/employees?cafe=${encodeURIComponent(cafeName)}`);
    setCafeFilter(cafeName);
  };

  const handleFilterChange = (event: SelectChangeEvent<string>) => {
    const newFilter = event.target.value;
    setCafeFilter(newFilter);
    // Update URL query parameter
    navigate(`/employees?cafe=${encodeURIComponent(newFilter)}`, {
      replace: true,
    });
  };

  if (loading) {
    return <LoadingScreen />;
  }

  return (
    <Box padding={2}>
      <Typography variant="h4" gutterBottom>
        Employees
      </Typography>
      <Box display="flex" alignItems="center" marginBottom={2}>
        <Select
          labelId="cafe-filter-label"
          id="cafe-filter"
          value={cafeFilter}
          onChange={handleFilterChange}
          displayEmpty
          style={{ marginRight: "16px", minWidth: 200 }}
        >
          <MenuItem value="">
            <span>All Cafes</span>
          </MenuItem>
          {cafes.map((cafe) => (
            <MenuItem key={cafe.id} value={cafe.name}>
              {cafe.name}
            </MenuItem>
          ))}
        </Select>
        <Button
          variant="contained"
          color="primary"
          onClick={handleAddEmployee}
          style={{ marginRight: "16px" }}
        >
          Add New Employee
        </Button>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => navigate("/cafes")}
        >
          Go to Cafes
        </Button>
      </Box>
      <EmployeeTable
        data={employees}
        onEdit={handleEditEmployee}
        onDelete={handleDeleteEmployee}
        onCafeClick={handleCafeClick}
      />
    </Box>
  );
};

export default EmployeesPage;
