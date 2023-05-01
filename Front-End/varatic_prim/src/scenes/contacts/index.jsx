import { Box } from "@mui/material";
import { DataGrid, GridToolbar } from "@mui/x-data-grid";
import { tokens } from "../../theme";
import { mockDataContacts } from "../../data/mockData";
import Header from "../../components/Header";
import { useTheme } from "@mui/material";
import { Button } from "@mui/material";
import { EditOutlined } from "@mui/icons-material";
import { AddOutlined } from "@mui/icons-material";
import { DeleteOutline } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

const Users = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  const navigate = useNavigate();

  function addUserHandler() {
      navigate("/user/add");
  }

  const columns = [
    { field: "id", headerName: "ID", flex: 0.5 },
    { field: "firstName", headerName: "Nume" },
    {
      field: "lastName",
      headerName: "Prenume",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    {
      field: "phone",
      headerName: "Telefon",
      type: "phone",
      headerAlign: "left",
      align: "left",
    },
    {
      field: "mobile",
      headerName: "Celular",
      flex: 1,
    },
    {
      field: "email",
      headerName: "Email",
      flex: 1,
    },
    {
      field: "address",
      headerName: "Adresa",
      flex: 1,
    },
    {
      field: "city",
      headerName: "Oras",
      flex: 1,
    },
    {
      field: "actions",
      headerName: "Actions",
      flex: 1,
      align: "left",
      headerAlign: "left",
      renderCell: () => {
        return (
          <Box width="70%" display={"flex"} justifyContent={"space-between"}>
            <Button style={{backgroundColor: "#eee9"}}>
              <EditOutlined />
            </Button>

            <Button style={{backgroundColor: "#E2403C"}}>
              <DeleteOutline />
            </Button>
          </Box>
        );
      }
    }
  ];

  return (
    <Box m="20px">
      <Header
        title="USERS"
        subtitle="Lista cu utilizatori si contacte"
      />

      <Box to="/user/add" width="100%" display={"flex"} justifyContent={"flex-end"}>
        <Button onClick={addUserHandler} style={{marginBottom: "-35px", backgroundColor: "#63993D", fontSize: "14px"}}>
          <AddOutlined /> <Box marginLeft={"10px"}>Add new user</Box> 
        </Button>
      </Box>

      <Box
        m="40px 0 0 0"
        height="75vh"
        sx={{
          "& .MuiDataGrid-root": {
            border: "none",
          },
          "& .MuiDataGrid-cell": {
            borderBottom: "none",
          },
          "& .name-column--cell": {
            color: colors.greenAccent[300],
          },
          "& .MuiDataGrid-columnHeaders": {
            backgroundColor: colors.blueAccent[700],
            borderBottom: "none",
          },
          "& .MuiDataGrid-virtualScroller": {
            backgroundColor: colors.primary[400],
          },
          "& .MuiDataGrid-footerContainer": {
            borderTop: "none",
            backgroundColor: colors.blueAccent[700],
          },
          "& .MuiCheckbox-root": {
            color: `${colors.greenAccent[200]} !important`,
          },
          "& .MuiDataGrid-toolbarContainer .MuiButton-text": {
            color: `${colors.grey[100]} !important`,
          },
        }}
      >
        <DataGrid
          rows={mockDataContacts}
          columns={columns}
          components={{ Toolbar: GridToolbar }}
        />
      </Box>
    </Box>
  );
};

export default Users;
