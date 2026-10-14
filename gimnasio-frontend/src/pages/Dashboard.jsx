import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import { apiFetch } from "../utils/api";

function Dashboard() {
  const { user, logout } = useContext(AuthContext);
  const [socios, setSocios] = useState([]);

  useEffect(() => {
    const fetchSocios = async () => {
      try {
        const data = await apiFetch(
          "https://localhost:7274/api/socios",
          {},
          user.token,
        );
        setSocios(data);
      } catch (error) {
        console.error(error.message);
        if (error.message === "Unauthorized") {
          logout();
        }
      }
    };
    if (user?.token && user?.role === "Administrador") {
      fetchSocios();
    }
  }, [user]);

  return (
    <div className="container mt-5">
      <h2>Bienvenido al Dashboard del Gimnasio</h2>
      {user && (
        <p>
          Hola {user.name}, tu rol es {user.role}
        </p>
      )}
      <button className="btn btn-danger" onClick={logout}>
        Cerrar Sesión
      </button>

      {user?.role === "Administrador" && (
        <>
          <h3 className="mb-3">Módulos de administración</h3>
          <div className="row ">
            <div className="col-md-3">
              <div className="card text-center mb-3">
                <div className="card-body">
                  <h5 className="card-title">Socios</h5>
                  <p className="card-text">Gestión Completa de Socios</p>
                </div>
              </div>
            </div>
            <div className="col-md-3">
              <div className="card text-center mb-3">
                <div className="card-body">
                  <h5 className="card-title">Pagos</h5>
                  <p className="card-text">Control de pagos y cuotas</p>
                </div>
              </div>
            </div>
            <div className="col-md-3">
              <div className="card text-center mb-3">
                <div className="card-body">
                  <h5 className="card-title">Horarios</h5>
                  <p className="card-text">Administrar clases y turnos</p>
                </div>
              </div>
            </div>
            <div className="col-md-3">
              <div className="card text-center mb-3">
                <div className="card-body">
                  <h5 className="card-title">Planes</h5>
                  <p className="card-text">Gestión de planes y membresías</p>
                </div>
              </div>
            </div>
          </div>

          <h3 className="mt-4">Listado de socios</h3>
          <table className="table table-striped mt-2">
            <thead>
              <tr>
                <th>Nombre Completo</th>
                <th>Email</th>
                <th>DNI</th>
                <th>Estado Membresia</th>
              </tr>
            </thead>
            <tbody>
              {socios.map((s) => (
                <tr key={s.id}>
                  <td>{s.nombreCompleto}</td>
                  <td>{s.email}</td>
                  <td>{s.dni}</td>
                  <td>{s.estadoMembresia ? "Activo" : "Inactivo"}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}

      {user?.role === "Socio" && (
        <div className="card mt-4">
          <div className="card-body">
            <h3 className="card-title">Perfil del Socio</h3>
            <p className="card-text">
              Aquí podés ver tus datos personales, tu plan actual y tus pagos.
            </p>
          </div>
        </div>
      )}
    </div>
  );
}

export default Dashboard;
