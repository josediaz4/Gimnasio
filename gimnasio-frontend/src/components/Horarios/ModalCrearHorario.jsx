import { useState } from "react";
import { diasSemana } from "../../utils/diasSemana";

function ModalCrearHorario({ onHorarioCreado }) {
  const API_URL = "https://localhost:7274/api/Horarios";

  const [nuevoHorario, setNuevoHorario] = useState({
    nombreActividad: "",
    diaSemana: "",
    horaInicio: "",
    horaFin: "",
    capacidadMaxima: 0,
  });

  const crearHorario = async () => {
    //Validaciones
    if (!nuevoHorario.nombreActividad) {
      alert("El campo es obligatorio");
      return;
    }
    if (!nuevoHorario.diaSemana) {
      alert("Debe seleccionar un dia de la semana");
      return;
    }
    if (nuevoHorario.horaInicio >= nuevoHorario.horaFin) {
      alert("La hora de inicio debe ser menor que la hora de fin");
      return;
    }
    //POST a la API
    await fetch(`${API_URL}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(nuevoHorario),
    });

    //Limpiar formulario
    setNuevoHorario({
      nombreActividad: "",
      diaSemana: "",
      horaInicio: "",
      horaFin: "",
      capacidadMaxima: 0,
    });

    //Avisar al padre que se creo un nuevo horario
    onHorarioCreado();
  };

  return (
    <div
      className="modal fade"
      id="modalCrearHorario"
      tabIndex="-1"
      aria-hidden="true"
    >
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Crear Nuevo Horario</h5>
            <button
              type="button"
              className="btn-close"
              data-bs-dismiss="modal"
            ></button>
          </div>
          <div className="modal-body">
            <form>
              <div className="mb-3">
                <label className="form-label">Actividad</label>
                <input
                  type="text"
                  className="form-control"
                  value={nuevoHorario.nombreActividad}
                  onChange={(e) =>
                    setNuevoHorario({
                      ...nuevoHorario,
                      nombreActividad: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Día de la Semana</label>
                <select
                  className="form-select"
                  value={nuevoHorario.diaSemana}
                  onChange={(e) =>
                    setNuevoHorario({
                      ...nuevoHorario,
                      diaSemana: e.target.value,
                    })
                  }
                  required
                >
                  <option value="">Seleccione un día</option>
                  {diasSemana.map((dia, index) => (
                    <option key={index} value={dia}>
                      {dia}
                    </option>
                  ))}
                </select>
              </div>
              <div className="mb-3">
                <label className="form-label">Hora Inicio</label>
                <input
                  type="time"
                  className="form-control"
                  value={nuevoHorario.horaInicio}
                  onChange={(e) =>
                    setNuevoHorario({
                      ...nuevoHorario,
                      horaInicio: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Hora Fin</label>
                <input
                  type="time"
                  className="form-control"
                  value={nuevoHorario.horaFin}
                  onChange={(e) =>
                    setNuevoHorario({
                      ...nuevoHorario,
                      horaFin: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Capacidad Máxima</label>
                <input
                  type="number"
                  className="form-control"
                  value={nuevoHorario.capacidadMaxima}
                  onChange={(e) =>
                    setNuevoHorario({
                      ...nuevoHorario,
                      capacidadMaxima: e.target.value,
                    })
                  }
                />
              </div>
            </form>
          </div>
          <div className="modal-footer">
            <button
              type="button"
              className="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Cancelar
            </button>
            <button
              type="button"
              className="btn btn-success"
              data-bs-dismiss="modal"
              onClick={crearHorario}
            >
              Guardar
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalCrearHorario;
