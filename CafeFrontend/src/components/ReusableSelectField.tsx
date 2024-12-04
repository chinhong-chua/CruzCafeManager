import React from 'react';
import { TextField, MenuItem } from '@mui/material';
import { Controller } from 'react-hook-form';

interface ReusableSelectFieldProps {
  name: string;
  control: any;
  label: string;
  options: { value: string; label: string }[];
  defaultValue?: string;
}

const ReusableSelectField: React.FC<ReusableSelectFieldProps> = ({
  name,
  control,
  label,
  options,
  defaultValue = '',
}) => (
  <Controller
    name={name}
    control={control}
    defaultValue={defaultValue}
    render={({ field, fieldState: { error } }) => (
      <TextField
        {...field}
        label={label}
        select
        fullWidth
        margin="normal"
        error={!!error}
        helperText={error ? error.message : ''}
      >
        {options.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </TextField>
    )}
  />
);

export default ReusableSelectField;
