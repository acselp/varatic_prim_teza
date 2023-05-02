import React, {useState} from "react";
import CustomInput from "../../components/CustomInput";
import {Box, Button, TextField} from "@mui/material";
import * as yup from "yup";
import {Formik} from "formik";
import {useNavigate} from "react-router-dom";
import Header from "../../components/Header";

function Login() {
    let [email, setEmail] = useState("");
    let [password, setPassword] = useState("");

    const handleFormSubmit = (values) => {
        console.log(values);
    };

    const navigate = useNavigate();

    return (
        <Box display={"flex"} alignItems={"center"} mt={"150px"} flexDirection={"column"}>

            <Header title="LOGIN USER" />

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
                  }) => (

                <form onSubmit={handleSubmit} style={{backgroundColor:"#101624", "padding": "20px", marginTop: "20px"}} className="form">
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
                        sx={{ gridColumn: "span 2" }}
                        style={{marginBottom: "20px"}}
                    />

                    <TextField
                        fullWidth
                        variant="filled"
                        type="password"
                        label="Password"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        value={values.lastName}
                        name="password"
                        error={!!touched.password && !!errors.password}
                        helperText={touched.password && errors.password}
                        sx={{ gridColumn: "span 2" }}
                    />

                    <Box width={"100%"} display="flex" justifyContent="center" mt="50px">
                        <Button type={"submit"} color="secondary" variant="contained" style={{fontSize: "14px", width: "50%"}}>
                            Log in
                        </Button>
                    </Box>
                </form>
                )}
            </Formik>
        </Box>
    );
}

const checkoutSchema = yup.object().shape({
    email: yup.string().required("required").email("invalid email"),
    password: yup.string().required("required"),
});

const initialValues = {
    email: "",
    password: "",
};

export default Login;