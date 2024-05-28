import React from "react";
import { Route, Routes } from "react-router-dom";
import Navbar from "./components/navbar/Navbar";
import { BrowserRouter } from "react-router-dom";
import { BothRoutes, InstructorRoutes, StudentRoutes } from "./Routes";
import Role from "./enums/Role";
import CookieManager from "./helpers/CookieManager";

function App() {
  var user = JSON.parse(CookieManager.getCookie("user"));

  return (
    <React.Fragment>
      {user && <Navbar />}
      <BrowserRouter>
        <Routes>
          {user &&
            user.role == Role.Instructor &&
            InstructorRoutes.map((r, i) => (
              <Route key={i} path={r.path} element={r.component} />
            ))}
          {user &&
            user.role == Role.Student &&
            StudentRoutes.map((r, i) => (
              <Route key={i} path={r.path} element={r.component} />
            ))}
          {BothRoutes.map((r, i) => (
            <Route key={i} path={r.path} element={r.component} />
          ))}
        </Routes>
      </BrowserRouter>
    </React.Fragment>
  );
}

export default App;
