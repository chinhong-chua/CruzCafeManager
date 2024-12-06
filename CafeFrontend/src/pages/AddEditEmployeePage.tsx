import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Typography,
  CircularProgress,
  TextField,
  MenuItem,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useForm, Controller } from "react-hook-form";
import {
  getEmployeeById,
  createEmployee,
  updateEmployee,
} from "../services/employeeService";
import { getCafes } from "../services/cafeService";
import { Employee, Cafe } from "../interfaces";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import ReusableTextField from "../components/ReusableTextField";
import ReusableSelectField from "../components/ReusableSelectField";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFnsV3";

interface FormValues {
  name: string;
  emailAddress: string;
  phoneNumber: string;
  gender: string;
  cafe?: string;
  startDate?: Date | null;
}

const genderOptions = [
  { value: "Male", label: "Male" },
  { value: "Female", label: "Female" },
  { value: "PreferNotToSay", label: "Prefer Not To Say" },
];

const schema = yup.object().shape({
  name: yup
    .string()
    .required("Name is required")
    .max(80, "Name must be at most 80 characters"),
  emailAddress: yup
    .string()
    .required("Email is required")
    .email("Invalid email address"),
  phoneNumber: yup
    .string()
    .required("Phone number is required")
    .matches(
      /^(8|9)\d{7}$/,
      "Phone number must start with 8 or 9 and have exactly 8 digits"
    ),
  gender: yup
    .string()
    .required("Gender is required")
    .oneOf(["Male", "Female", "PreferNotToSay"], "Invalid gender value"),
  cafe: yup.string(),
  startDate: yup
    .date()
    .nullable()
    .min(new Date(2010, 0, 1), 'Start date must be on or after January 1, 2010'),
});

const AddEditEmployeePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const isEditMode = !!id;
  const navigate = useNavigate();
  const [loading, setLoading] = useState<boolean>(false);
  const [cafes, setCafes] = useState<Cafe[]>([]);
  const [employee, setEmployee] = useState<Employee | null>(null);

  const {
    control,
    handleSubmit,
    reset,
    formState: { isSubmitting, errors },
  } = useForm<FormValues>({
    resolver: yupResolver(schema),
  });

  useEffect(() => {
    const loadData = async () => {
      setLoading(true);
      try {
        const cafesResponse = await getCafes();
        setCafes(cafesResponse.data);

        if (isEditMode) {
          const employeeResponse = await getEmployeeById(id!);
          const employeeData = employeeResponse.data;
          setEmployee(employeeData);

          const cafeId =
            cafesResponse.data.find((c) => c.name === employeeData.cafe)?.id ||
            "";

          reset({
            name: employeeData.name,
            emailAddress: employeeData.emailAddress,
            phoneNumber: employeeData.phoneNumber,
            gender: employeeData.gender,
            cafe: cafeId,
            startDate: employeeData.startDate
              ? new Date(employeeData.startDate)
              : null,
          });
        } else {
          reset();
        }
      } catch (error) {
        console.error("Failed to fetch data", error);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [id, isEditMode]);

  const onSubmit = async (data: FormValues) => {
    console.log("submitting... ", data);
    const employeeData: Employee = {
      id: isEditMode ? id! : "",
      name: data.name,
      emailAddress: data.emailAddress,
      phoneNumber: data.phoneNumber,
      gender: data.gender,
      cafe: data.cafe,
      daysWorked: employee?.daysWorked || 0,
      startDate: data.startDate ? data.startDate.toISOString() : null,
    };

    try {
      if (isEditMode) {
        await updateEmployee(id!, employeeData);
      } else {
        await createEmployee(employeeData);
      }
      navigate("/employees");
    } catch (error) {
      console.error("Failed to save employee", error);
    }
  };

  const handleCancel = () => {
    navigate("/employees");
  };

  if (loading) {
    return (
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        minHeight="50vh"
      >
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box padding={2}>
      <Typography variant="h4" gutterBottom>
        {isEditMode ? "Edit Employee" : "Add New Employee"}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <ReusableTextField name="name" control={control} label="Name" />
        <ReusableTextField
          name="emailAddress"
          control={control}
          label="Email Address"
        />
        <ReusableTextField
          name="phoneNumber"
          control={control}
          label="Phone Number"
        />
        <ReusableSelectField
          name="gender"
          control={control}
          label="Gender"
          options={genderOptions}
        />
        <Controller
          name="cafe"
          control={control}
          render={({ field }) => (
            <TextField
              {...field}
              label="Cafe"
              select
              fullWidth
              margin="normal"
              error={!!errors.cafe}
              helperText={errors.cafe ? errors.cafe.message : ""}
            >
              {cafes.map((cafe) => (
                <MenuItem key={cafe.id} value={cafe.id}>
                  {cafe.name}
                </MenuItem>
              ))}
            </TextField>
          )}
        />
        <Box marginTop={2}>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <Controller
              name="startDate"
              control={control}
              render={({ field }) => (
                <DatePicker
                  label="Start Date"
                  value={field.value}
                  onChange={(date) => field.onChange(date)}
                  format="dd/MM/yyyy" 
                  minDate={new Date(2000, 0, 1)} // Set minimum date to January 1, 2000
                  maxDate={new Date()}
                  slotProps={{
                    textField: {
                      fullWidth: true,
                      error: !!errors.startDate, // Set error state
                      helperText: errors.startDate
                        ? errors.startDate.message
                        : "",
                    },
                  }}
                ></DatePicker>
              )}
            />
          </LocalizationProvider>
        </Box>
        <Box marginTop={2}>
          <Button
            type="submit"
            variant="contained"
            color="primary"
            disabled={isSubmitting}
          >
            {isSubmitting ? "Submitting..." : "Submit"}
          </Button>
          <Button
            variant="outlined"
            onClick={handleCancel}
            style={{ marginLeft: "10px" }}
          >
            Cancel
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default AddEditEmployeePage;
