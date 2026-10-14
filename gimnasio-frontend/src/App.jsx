import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Socios from "./pages/Socios";
import Perfil from "./pages/Perfil";
import Pagos from "./pages/Pagos";
import Horarios from "./pages/Horarios";
import Planes from "./pages/Planes";

import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import PrivateRoute from "./components/PrivateRoute";
import { AuthContext, AuthProvider } from "./context/AuthContext";
import { useContext } from "react";
import Navbar from "./components/Navbar";

function AppRoutes() {
  const { user } = useContext(AuthContext);

  return (
    <>
      {user && <Navbar />}
      <Routes>
        <Route
          path="/"
          element={
            user ? <Navigate to="/dashboard" /> : <Navigate to="/login" />
          }
        />
        <Route path="/login" element={<Login />} />
        {/*Rutas protegidas */}
        <Route
          path="/dashboard"
          element={
            <PrivateRoute>
              <Dashboard />
            </PrivateRoute>
          }
        />
        {/*Admin */}
        <Route
          path="/socios"
          element={
            <PrivateRoute roles={["Administrador"]}>
              <Socios />
            </PrivateRoute>
          }
        />
        <Route
          path="/pagos"
          element={
            <PrivateRoute roles={["Administrador"]}>
              <Pagos />
            </PrivateRoute>
          }
        />
        <Route
          path="/horarios"
          element={
            <PrivateRoute roles={["Administrador"]}>
              <Horarios />
            </PrivateRoute>
          }
        />
        <Route
          path="/planes"
          element={
            <PrivateRoute roles={["Administrador"]}>
              <Planes />
            </PrivateRoute>
          }
        />

        {/*Socio */}
        <Route
          path="/perfil"
          element={
            <PrivateRoute roles={["Socio"]}>
              <Perfil />
            </PrivateRoute>
          }
        />
        <Route path="*" element={<Navigate to="/login" />} />
      </Routes>
    </>
  );
}

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
