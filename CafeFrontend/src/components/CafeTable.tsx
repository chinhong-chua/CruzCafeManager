import React from "react";
import { AgGridReact } from "ag-grid-react";
import { Button } from "@mui/material";
import { ColDef } from "ag-grid-community";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";
import { Cafe } from "../interfaces";

interface CafeTableProps {
  data: Cafe[];
  onEdit: (id: string) => void;
  onDelete: (id: string) => void;
  onEmployeesClick: (cafeName: string) => void;
}

const CafeTable: React.FC<CafeTableProps> = ({
  data,
  onEdit,
  onDelete,
  onEmployeesClick,
}) => {
  const columns: ColDef[] = [
    {
      headerName: "Name",
      field: "name",
      sortable: true,
      filter: true,
    },
    { headerName: "Location", field: "location" },
    {
      headerName: "Logo",
      field: "logo",
      autoHeight: true,
      width:250,
      cellRenderer: (params: any) => {
        if (params.value) {
        //   const src = `data:image/png;base64,${params.value}`;
          // const src = 'https://picsum.photos/200/300'
          return <img src={params.value} alt="Logo" style={{ width: "200px" }} />;
        }
        return null;
      },
    },

    { headerName: "Description", field: "description",minWidth:200, flex:1, wrapText: true },
    {
      headerName: "Employees",
      field: "employees",
      cellRenderer: (params: any) => (
        <Button
          variant="text"
          onClick={() => onEmployeesClick(params.data.name)}
        >
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

export default CafeTable;
