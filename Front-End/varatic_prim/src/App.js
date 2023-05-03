import { useState } from "react";
import { Routes, Route } from "react-router-dom";
import Topbar from "./scenes/global/Topbar";
import Sidebar from "./scenes/global/Sidebar";
import Dashboard from "./scenes/dashboard";
import Team from "./scenes/team";
import Invoices from "./scenes/invoices";
import Users from "./scenes/contacts";
import Bar from "./scenes/bar";
import Form from "./scenes/CreateUser/CreateUser";
import Line from "./scenes/line";
import Pie from "./scenes/pie";
import FAQ from "./scenes/faq";
import Geography from "./scenes/geography";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./theme";
import Calendar from "./scenes/calendar/calendar";
import Login from "./scenes/authentication/Login";
import {RequireAuth, useIsAuthenticated} from "react-auth-kit";

function App() {
  const [theme, colorMode] = useMode();
  const [isSidebar, setIsSidebar] = useState(true);

    return (
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <div className="app">
            <Sidebar isSidebar={isSidebar} />
            <main className="content">
              <Topbar setIsSidebar={setIsSidebar} />
              <Routes>


                <Route path="/login" element={<Login />} />

                <Route path="/" element={
                    <RequireAuth loginPath={"/login"}>
                      <Dashboard />
                    </RequireAuth>
                } />

                <Route path="/users" element={
                  <RequireAuth loginPath={"/login"}>
                    <Users />
                  </RequireAuth>
                } />

                <Route path="/user/add" element={
                  <RequireAuth loginPath={"/login"}>
                    <Form />
                  </RequireAuth>
                } />



                <Route path="/invoices" element={<Invoices />} />

                <Route path="/bar" element={<Bar />} />

                <Route path="/pie" element={<Pie />} />

                <Route path="/line" element={<Line />} />

                <Route path="/faq" element={<FAQ />} />

                <Route path="/calendar" element={<Calendar />} />

                <Route path="/geography" element={<Geography />} />



              </Routes>
            </main>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    );
}

export default App;
