import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import { apiFetch } from "../utils/api";
import ModalCrearSocio from "../components/Socios/ModalCrearSocio";

function Socios() {
  const API_URL = "https://localhost:7274/api/Socios";

  const { user, logout } = useContext(AuthContext);
  const [socios, setSocios] = useState([]);

  const cargarSocios = async () => {
    try {
      const respuesta = await fetch(API_URL, {}, user.token);
      const data = await respuesta.json();
      setSocios(data);
    } catch (error) {
      console.error(error.message);
      if (error.message == "Unautorized") logout();
    }
  };

  useEffect(() => {
    // const fetchSocios = async () => {
    //   try {
    //     const data = await apiFetch(
    //       "https://localhost:7274/api/socios",
    //       {},
    //       user.token,
    //     );
    //     setSocios(data);
    //   } catch (error) {
    //     console.error(error.message);
    //     if (error.message === "Unautorized") logout();
    //   }
    // };
    cargarSocios();
    if (user?.token && user?.role === "Administrador") cargarSocios();
  }, [user]);

  return (
    <div className="container mt-4">
      <h2 className="mb-3">Gestión de socios</h2>
      <div className="card shadow">
        <div className="card-body">
          {/*Boton para abrir el modal */}
          <div className="d-flex justify-content-end mt-4">
            <button
              className="btn btn-primary"
              data-bs-toggle="modal"
              data-bs-target="#modalCrearSocio"
            >
              ➕ Nuevo Socio
            </button>
            {/* Modal de creación */}
            <ModalCrearSocio onSocioCreado={cargarSocios} />
          </div>
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
