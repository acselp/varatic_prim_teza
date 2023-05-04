import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import { BrowserRouter } from "react-router-dom";
import {AuthProvider} from "react-auth-kit";
import refreshApi from "./api/refreshApi";

const root = ReactDOM.createRoot(document.getElementById("root"));


root.render(
  <React.StrictMode>
      <AuthProvider
          authType={"cookie"}
          authName={"_auth"}
          cookieDomain={window.location.hostname}
          cookieSecure={false}
          refresh={refreshApi}
      >
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </AuthProvider>
  </React.StrictMode>
);
