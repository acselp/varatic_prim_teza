import React, {useState} from "react";
import CustomInput from "../../components/CustomInput";
import {Box, Button, TextField} from "@mui/material";
import * as yup from "yup";
import {Formik} from "formik";
import {useNavigate} from "react-router-dom";
import Header from "../../components/Header";
import axios, {AxiosError} from "axios";
import {useSignIn} from "react-auth-kit";
import {diffMinutes} from "../../TimeService/Time";

function Login() {
    let [email, setEmail] = useState("");
    let [password, setPassword] = useState("");
    let [error, setError] = useState("");
    const signIn = useSignIn();
    const navigate = useNavigate();
    const handleFormSubmit = async (values) => {

        try {
            const response = await axios.post(
                "http://localhost:5000/authentication/login",
                {email: values.email, password: values.password}
            )


            signIn({
                token: response.data.accessToken,
                expiresIn: 14,         //Minutes
                tokenType: "Bearer",
                authState: { email: values.email },
                refreshToken: response.data.refreshToken,
                refreshTokenExpireIn: 359
            })
            console.log(response);

            navigate("/");
        }
        catch(err) {
            if (err && err instanceof AxiosError) {
                setError(err.response?.data.message);
                console.log(err.response?.data.message);
            }


            else if (err && err instanceof Error) {
                setError(err.message);
                console.log(err.message);
            }
        }
    };

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