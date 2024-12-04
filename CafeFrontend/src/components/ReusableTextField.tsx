import React from 'react';
import { TextField } from '@mui/material';
import { Controller } from 'react-hook-form';

interface ReusableTextFieldProps {
  name: string;
  control: any;
  label: string;
  defaultValue?: string;
  rules?: any;
  multiline?: boolean;
  rows?: number;
}

const ReusableTextField: React.FC<ReusableTextFieldProps> = ({
  name,
  control,
  label,
  defaultValue = '',
  rules = {},
  multiline = false,
  rows = 1,
}) => (
  <Controller
    name={name}
    control={control}
    defaultValue={defaultValue}
    rules={rules}
    render={({ field, fieldState: { error } }) => (
      <TextField
        {...field}
        label={label}
        error={!!error}
        helperText={error ? error.message : ''}
        fullWidth
        margin="normal"
        multiline={multiline}
        rows={rows}
      />
    )}
  />
);

export default ReusableTextField;
