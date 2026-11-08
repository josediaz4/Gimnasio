import { useEffect, useState } from "react";
import ModalCrearPlan from "../components/Planes/ModalCrearPlan";
import ModalEditarPlan from "../components/Planes/ModalEditarPlan";

function Planes() {
  const API_URL = "https://localhost:7274/api/PlanesPago";
  const [planes, setPlanes] = useState([]);

  //Cargar Planes
  const cargarPlanes = async () => {
    const respuesta = await fetch(API_URL);
    const data = await respuesta.json();
    setPlanes(data);
  };

  useEffect(() => {
    cargarPlanes();
  }, []);

  const eliminarPlan = async (id) => {
    await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });
    cargarPlanes();
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-3">📦 Gestión de Planes</h2>
      <div className="card shadow">
        <div className="card-body">
          <div className="row mt-3">
            <div className="col-md-6">
              <p className="card-text">
                Define los planes y membresías disponibles para los socios.
              </p>
            </div>
            {/*Boton para abrir el modal */}
            <div className="col-md-6 d-flex justify-content-end">
              <button
                className="btn btn-primary"
                data-bs-toggle="modal"
                data-bs-target="#modalCrearPlan"
              >
                ➕Nuevo Plan
              </button>
              {/* Modal de creación */}
              <ModalCrearPlan onPlanCreado={cargarPlanes} />
            </div>
          </div>
          <div className="row mt-3">
            {planes.map((p) => (
              <div className="col-md-4 mb-3">
                <div className="card text-center">
                  <div className="card-body">
                    <h5 className="card-title">{p.nombrePlan}</h5>
                    <p className="card-text">
                      Cantidad de dias: {p.cantidadDias}
                    </p>
                    <h4 className="card-text">${p.precio} /mes</h4>
                    {/* Botón Editar abre modal específico */}
                    <button
                      className="btn btn-outline-success me-2"
                      data-bs-toggle="modal"
                      data-bs-target={`#modalEditar-${p.idPlan}`}
                    >
                      Editar
                    </button>
                    <button
                      className="btn btn-outline-danger"
                      onClick={() => eliminarPlan(p.idPlan)}
                    >
                      Eliminar
                    </button>
                    {/* Modal de edición para este plan */}
                    <ModalEditarPlan
                      plan={p}
                      onPlanActualizado={cargarPlanes}
                    />
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Planes;
