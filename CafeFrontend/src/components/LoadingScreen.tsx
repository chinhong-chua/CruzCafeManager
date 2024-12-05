import { Box, CircularProgress } from "@mui/material";
import React from "react";

const LoadingScreen: React.FC = () => {
  return (
    <div>
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        minHeight="50vh"
      >
        <CircularProgress />
      </Box>
    </div>
  );
};

export default LoadingScreen;
