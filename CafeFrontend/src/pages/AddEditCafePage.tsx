import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Typography,
  CircularProgress,
  IconButton,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { useForm } from "react-hook-form";
import { getCafeById, createCafe, updateCafe } from "../services/cafeService";
import { Cafe } from "../interfaces";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import ReusableTextField from "../components/ReusableTextField";
import PhotoCameraBackIcon from "@mui/icons-material/PhotoCameraBack";

interface FormValues {
  name: string;
  description: string;
  location: string;
  logo?: FileList;
}

const schema = yup.object().shape({
  name: yup
    .string()
    .required("Name is required")
    .min(6, "Name must be at least 6 characters")
    .max(10, "Name must be at most 10 characters"),
  description: yup
    .string()
    .required("Description is required")
    .min(5, "Description must be at least 5 characters")
    .max(256, "Description must be at most 256 characters"),
  location: yup.string().required("Location is required"),
});

const AddEditCafePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const isEditMode = !!id;
  const navigate = useNavigate();
  const [loading, setLoading] = useState<boolean>(false);
  const [logoPreview, setLogoPreview] = useState<string | undefined>(undefined);

  const {
    control,
    handleSubmit,
    reset,
    setValue,
    formState: { isSubmitting, errors },
  } = useForm<FormValues>({
    resolver: yupResolver(schema),
  });

  useEffect(() => {
    if (isEditMode) {
      setLoading(true);
      getCafeById(id!)
        .then((response) => {
          const cafe = response.data;
          reset({
            name: cafe.name,
            description: cafe.description,
            location: cafe.location,
          });
          if (cafe.logo) {
            setLogoPreview(cafe.logo);
          }
        })
        .catch((error) => {
          console.error("Failed to fetch cafe", error);
        })
        .finally(() => setLoading(false));
    }
    return () => {
      if (logoPreview && logoPreview.startsWith("blob:")) {
        URL.revokeObjectURL(logoPreview);
      }
    };
  }, [id, isEditMode]);

  const onSubmit = async (data: FormValues) => {
    let logoBase64: string | undefined = undefined;

    if (data.logo && data.logo.length > 0) {
      const file = data.logo[0];
      logoBase64 = await convertFileToBase64(file);
    }

    const cafeData: Cafe = {
      id: isEditMode ? id! : "",
      name: data.name,
      description: data.description,
      location: data.location,
      logo: logoBase64,
    };

    try {
      if (isEditMode) {
        await updateCafe(id!, cafeData);
      } else {
        await createCafe(cafeData);
      }
      navigate("/cafes");
    } catch (error) {
      console.error("Failed to save cafe", error);
    }
  };

  const convertFileToBase64 = (file: File): Promise<string> => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = (error) => reject(error);
    });
  };

  const handleLogoChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    setValue("logo", files || undefined);
    if (files && files.length > 0) {
      const file = files[0];

      const previewUrl = URL.createObjectURL(file);
      setLogoPreview(previewUrl);
    }
  };

  const handleCancel = () => {
    navigate("/cafes");
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
        {isEditMode ? "Edit Cafe" : "Add New Cafe"}
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <ReusableTextField name="name" control={control} label="Name" />
        <ReusableTextField
          name="description"
          control={control}
          label="Description"
          multiline
          rows={4}
        />
        <ReusableTextField name="location" control={control} label="Location" />

        {logoPreview && (
          <Box marginTop={2}>
            <Typography variant="subtitle1">Logo:</Typography>
            <img
              src={logoPreview}
              alt="Logo"
              style={{ width: "200px" }}
            />
          </Box>
        )}

        <Box marginTop={2}>
          <input
            accept="image/*"
            style={{ display: "none" }}
            id="logo-upload"
            type="file"
            onChange={handleLogoChange}
          />
          <label htmlFor="logo-upload">
            <Button
              color="success"
              variant="contained"
              component="span"
              startIcon={<PhotoCameraBackIcon />}
            >
              {isEditMode && logoPreview ? "Change Logo" : "Upload Logo"}
            </Button>
          </label>
          {errors.logo && (
            <Typography color="error">{errors.logo.message}</Typography>
          )}
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

export default AddEditCafePage;
