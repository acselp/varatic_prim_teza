import {Box, Button, Checkbox, FormControlLabel, TextField} from "@mui/material";
import {ErrorMessage, Formik} from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import Header from "../../components/Header";
import { ArrowBackOutlined } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { AxiosError } from "axios";
import { useState } from "react";
import MyAxios from "../../api/axios";

const Form = () => {
  const isNonMobile = useMediaQuery("(min-width:600px)");
  let [error, setError] = useState("");
  let [repeatCreateUser, setRepreatCreateUser] = useState(false);


  const handelChange = () => {
    setRepreatCreateUser(!repeatCreateUser);
  }
    const handleReset = (resetForm) => {
      resetForm();
    }
  const handleFormSubmit = async (values) => {
      try {

          console.log(values);

          const response = await MyAxios.post(
              "/user",
              {
                  email: values.email,
                  password: values.password,
                  contact: {
                      firstName: values.firstName,
                      lastName: values.lastName,
                      phone: values.phone,
                      mobile: values.mobile
                  }
              }
          )

          if(!repeatCreateUser) {
              navigate("/users");
          }
          else {
              navigate("/user/add");
          }
      }
      catch(err) {
          if (err && err instanceof AxiosError) {
              setError(err.response?.data.message);
              console.log(err.response?.data.message);
          }

          else if (err && err instanceof Error){
              setError(err.message);
              console.log(err.message);
          }
      }
  };

  const navigate = useNavigate();

  function backUserHandler() {
      navigate("/users");
  }



  return (
    <Box m="20px">
        <Header title="CREATE USER" subtitle="Create a New User Profile" />



        <Box marginBottom={"25px"} width="100%" display={"flex"} justifyContent={"flex-end"}>
            <FormControlLabel
                style={{color: "#141B2D", backgroundColor: "#0075F2", paddingRight: "15px", borderRadius: "5px"}}
                control={
                    <Checkbox checked={repeatCreateUser} onChange={handelChange} />
                }
                label="Adaugare repetata"
            />
            <Button onClick={backUserHandler} style={{backgroundColor: "#0075F2", fontSize: "14px"}}>
              <ArrowBackOutlined /> <Box marginLeft={"10px"}>Back to list</Box>
            </Button>
        </Box>

      <Formik
        onSubmit={handleFormSubmit}
        initialValues={initialValues}
        validationSchema={checkoutSchema}
      >

        {({
              values,
              errors,
              touched,
              handleBlur,
              handleChange,
              handleSubmit,
              resetForm,
            setFormikState
        }) => (
          <form onSubmit={handleSubmit}>
              <Box color={"red"}>
                  {error ? error : ""}
              </Box>
            <Box
              display="grid"
              gap="30px"
              gridTemplateColumns="repeat(4, minmax(0, 1fr))"
              sx={{
                "& > div": { gridColumn: isNonMobile ? undefined : "span 4" },
              }}
            >
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="First Name"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.firstName}
                name="firstName"
                error={!!touched.firstName && !!errors.firstName}
                helperText={touched.firstName && errors.firstName}
                sx={{ gridColumn: "span 2" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Last Name"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.lastName}
                name="lastName"
                error={!!touched.lastName && !!errors.lastName}
                helperText={touched.lastName && errors.lastName}
                sx={{ gridColumn: "span 2" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Email"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.email}
                name="email"
                error={!!touched.email && !!errors.email}
                helperText={touched.email && errors.email}
                sx={{ gridColumn: "span 4" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="password"
                label="Password"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.password}
                name="password"
                error={!!touched.password && !!errors.password}
                helperText={touched.password && errors.password}
                sx={{ gridColumn: "span 4" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="password"
                label="Re-enter Password"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.re_password}
                name="re_password"
                error={!!touched.re_password && !!errors.re_password}
                helperText={touched.re_password && errors.re_password}
                sx={{ gridColumn: "span 4" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="phone"
                label="Phone"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.phone}
                name="phone"
                error={!!touched.phone && !!errors.phone}
                helperText={touched.phone && errors.phone}
                sx={{ gridColumn: "span 4" }}
              />
              <TextField
                fullWidth
                variant="filled"
                type="phone"
                label="Mobile"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.mobile}
                name="mobile"
                error={!!touched.mobile && !!errors.mobile}
                helperText={touched.mobile && errors.mobile}
                sx={{ gridColumn: "span 4" }}
              />
            </Box>
            <Box display="flex" justifyContent="end" mt="20px">
              <Button type="submit" onClick={handleReset.bind(null, resetForm)}
                      color="secondary" variant="contained" style={{fontSize: "14px"}}>
                Create New User
              </Button>
            </Box>
          </form>
        )}
      </Formik>
    </Box>
  );
};

const phoneRegExp =
  /^((373|0)([0-9]){8})$/;

const checkoutSchema = yup.object().shape({
  firstName: yup.string().required("required"),
  lastName: yup.string().required("required"),
  email: yup.string().email("invalid email").required("required"),
  password: yup.string().required("required"),
  re_password: yup.string().required("required").oneOf([yup.ref('password'), null], 'Passwords must match'),
  phone: yup.string().matches(phoneRegExp, "Phone number is not valid").required("required"),
  mobile: yup.string().matches(phoneRegExp, "Mobile number is not valid").required("required")
});

const initialValues = {
  firstName: "",
  lastName: "",
  email: "",
  password: "",
  phone: "",
  mobile: "",
};

export default Form;
