import {Box, LinearProgress} from "@mui/material";
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
import {useEffect, useState} from "react";
import MyAxios from "../../api/axios";
import {useAuthHeader} from "react-auth-kit";
const Users = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  let [userData, setUserData] = useState([]);
  let [pageIndex, setPageIdex] = useState(0);
  let [pageSize, setPageSize] = useState(100);
  const authHeader = useAuthHeader();
  let [isLoading, setIsLoading] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    getAndSetUserData();
  }, [])

    function getAndSetUserData() {
        setIsLoading(true);
        const result = MyAxios.get(
            `/user?pageIndex=${pageIndex}&pageSize=${pageSize}`, {
                headers: {"Authorization": authHeader()}
            })
            .then((res) => {
                setUserData(mapUserData(res.data.data));
            })
        setIsLoading(false);
    }

function mapUserData(users){

    let result = [];

    users.map((user) => {
        let data = {
            id: user.id,
            email: user.email,
            firstName: user.contact.firstName,
            lastName: user.contact.lastName,
            phone: user.contact.phone,
            mobile: user.contact.mobile
        }

        result.push(data);
    })

    return result;
}

  function addUserHandler() {
      navigate("/user/add");
  }

  function handleEdit(id) {

  }

  function handleDelete(id) {
      MyAxios.delete(
          `/user/${id}`,
          {
              headers: {"Authorization": authHeader()}
          })
          .then((res) => {
              navigate("/users");
          })

      let data = userData;
      let data2 = data.filter((user) => {
          return user.id !== id;
      })

      setUserData(data2);
  }

  const columns = [
    { field: "id", headerName: "ID", flex: 0.5 },
    {
        field: "firstName",
        headerName: "Nume",
        flex: 1,
    },
    {
      field: "lastName",
      headerName: "Prenume",
      cellClassName: "name-column--cell",
        flex: 1,
    },
    {
      field: "phone",
      headerName: "Telefon",
      type: "phone",
      headerAlign: "left",
      align: "left",
        flex: 1,
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
      field: "actions",
      headerName: "Actions",
      flex: 1,
      align: "left",
      headerAlign: "left",
      renderCell: (params) => {
        return (
          <Box width="70%" display={"flex"}>
            <Button style={{backgroundColor: "#eee9", marginRight: "10px"}}>
              <EditOutlined />
            </Button>

            <Button onClick={() => {handleDelete(params.row.id)}} style={{backgroundColor: "#E2403C"}}>
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
            slots={{
                loadingOverlay: LinearProgress
            }}
            rows={userData}
            columns={columns}
            components={{ Toolbar: GridToolbar }}
            onPageSizeChange={size => {
              setPageSize(size);
              //getAndSetUserData();
            }}
            pageSize={pageSize}
            onPageChange={(page) => {
              setPageIdex(page - 1);
              //getAndSetUserData();
            }}
        />
      </Box>
    </Box>
  );
};

export default Users;
