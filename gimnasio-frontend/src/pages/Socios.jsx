import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import { apiFetch } from "../utils/api";

function Socios() {
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
        if (error.message === "Unautorized") logout();
      }
    };
    if (user?.token && user?.role === "Administrador") fetchSocios();
  }, [user]);

  return (
    <div className="container mt-4">
      <h2 className="mb-3">Gestión de socios</h2>
      <div className="card shadow">
        <div className="card-body">
          <p className="card-text">
            Aquí podrás administrar el listado de socios, agregar nuevos y
            editar información existente.
          </p>
          <table className="table table-striped mt-3">
            <thead>
              <tr>
                <th>Nombre Completo</th>
                <th>Email</th>
                <th>DNI</th>
                <th>Estado Membresia</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {socios.map((s) => (
                <tr key={s.id}>
                  <td>{s.nombreCompleto}</td>
                  <td>{s.email}</td>
                  <td>{s.dni}</td>
                  <td>{s.estadoMembresia ? "Activo" : "Inactivo"}</td>
                  <td>
                    <button className="btn btn-sm btn-primary me-2">
                      Editar
                    </button>
                    <button className="btn btn-sm btn-danger me-2">
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Socios;
