import React from "react";
import { AgGridReact } from "ag-grid-react";
import { Button } from "@mui/material";
import { ColDef } from "ag-grid-community";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import { Employee } from "../interfaces";

interface EmployeeTableProps {
  data: Employee[];
  onEdit: (id: string) => void;
  onDelete: (id: string) => void;
  onCafeClick: (cafeName: string) => void;
}

const EmployeeTable: React.FC<EmployeeTableProps> = ({
  data,
  onEdit,
  onDelete,
  onCafeClick,
}) => {
  const columns: ColDef[] = [
    { headerName: "ID", field: "id", sortable: true, filter: true },
    { headerName: "Name", field: "name", sortable: true, filter: true },
    { headerName: "Email", field: "emailAddress" },
    { headerName: "Phone", field: "phoneNumber" },
    { headerName: "Days Worked", field: "daysWorked", sortable: true, flex:1 },
    {
      headerName: "Cafe",
      field: "cafe",
      cellRenderer: (params: any) => (
        <Button variant="text" onClick={() => onCafeClick(params.value)}>
          {params.value}
        </Button>
      ),
    },
    {
      headerName: "Actions",
      field: "id",
      cellRenderer: (params: any) => (
        <>
          <Button variant="outlined" onClick={() => onEdit(params.value)}>
            Edit
          </Button>
          <Button
            variant="outlined"
            color="error"
            onClick={() => onDelete(params.value)}
            style={{ marginLeft: "8px" }}
          >
            Delete
          </Button>
        </>
      ),
    },
  ];

  return (
    <div className="ag-theme-alpine">
      <div style={{ width: "100%", height: "100%" }}>
        <AgGridReact
          rowData={data}
          columnDefs={columns}
          domLayout="autoHeight"
        />
      </div>
    </div>
  );
};

export default EmployeeTable;
