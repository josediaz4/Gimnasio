import { useEffect, useState } from "react";
import ModalEditarHorario from "../components/Horarios/ModalEditarHorario";
import ModalCrearHorario from "../components/Horarios/ModalCrearHorario";

function Horarios() {
  const API_URL = "https://localhost:7274/api/Horarios";
  const [horarios, setHorarios] = useState([]);

  //Cargar horarios
  const cargarHorarios = async () => {
    const respuesta = await fetch(API_URL);
    const data = await respuesta.json();
    setHorarios(data);
  };

  useEffect(() => {
    cargarHorarios();
  }, []);

  //Eliminar horarios
  const eliminarHorario = async (id) => {
    await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });
    cargarHorarios();
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-4">Administración de Horarios</h2>
      <div className="card shadow">
        <div className="card-body">
          {/*Boton para abrir el modal */}
          <div className="d-flex justify-content-end mt-4">
            <button
              className="btn btn-primary"
              data-bs-toggle="modal"
              data-bs-target="#modalCrearHorario"
            >
              ➕ Nuevo Horario
            </button>
            {/* Modal de creación */}
            <ModalCrearHorario onHorarioCreado={cargarHorarios} />
          </div>

          <table className="table table-striped table-hover">
            <thead className="table dark">
              <tr>
                <th>Actividad</th>
                <th>Día</th>
                <th>Inicio</th>
                <th>Fin</th>
                <th>Capacidad</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {horarios.map((h) => (
                <tr key={h.idHorario}>
                  <td>{h.nombreActividad}</td>
                  <td>{h.diaSemana}</td>
                  <td>{h.horaInicio}</td>
                  <td>{h.horaFin}</td>
                  <td>{h.capacidadMaxima}</td>
                  <td>
                    {/* Botón Editar abre modal específico */}
                    <button
                      className="btn btn-warning btn-sm me-2"
                      data-bs-toggle="modal"
                      data-bs-target={`#modalEditar-${h.idHorario}`}
                    >
                      Editar
                    </button>
                    <button
                      className="btn btn-danger btn-sm"
                      onClick={() => eliminarHorario(h.idHorario)}
                    >
                      Eliminar
                    </button>
                    {/* Modal de edición para este horario */}
                    <ModalEditarHorario
                      horario={h}
                      onHorarioActualizado={cargarHorarios}
                    />
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

export default Horarios;
