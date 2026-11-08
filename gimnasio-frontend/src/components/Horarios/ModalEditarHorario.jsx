import { useState } from "react";
import { diasSemana } from "../../utils/diasSemana";

function ModalEditarHorario({ horario, onHorarioActualizado }) {
  const API_URL = "https://localhost:7274/api/horarios";

  const [horarioEditado, setHorarioEditado] = useState(horario);

  const editarHorario = async () => {
    if (!horarioEditado.diaSemana) {
      alert("Debe seleccionar un dia de la semana");
      return;
    }
    if (horarioEditado.horaInicio >= horarioEditado.horaFin) {
      alert("La hora de inicio debe ser menor que la hora de fin");
      return;
    }

    await fetch(`${API_URL}/${horarioEditado.idHorario}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(horarioEditado),
    });

    //Avisar al padre que se actualizó
    onHorarioActualizado();
  };

  return (
    <div
      className="modal fade"
      id={`modalEditar-${horario.idHorario}`}
      tabIndex="-1"
    >
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Editar Horario</h5>
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
                  value={horarioEditado.nombreActividad}
                  onChange={(e) =>
                    setHorarioEditado({
                      ...horarioEditado,
                      nombreActividad: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Día de la Semana</label>
                <select
                  className="form-select"
                  value={horarioEditado.diaSemana}
                  onChange={(e) =>
                    setHorarioEditado({
                      ...horarioEditado,
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
                  value={horarioEditado.horaInicio}
                  onChange={(e) =>
                    setHorarioEditado({
                      ...horarioEditado,
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
                  value={horarioEditado.horaFin}
                  onChange={(e) =>
                    setHorarioEditado({
                      ...horarioEditado,
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
                  value={horarioEditado.capacidadMaxima}
                  onChange={(e) =>
                    setHorarioEditado({
                      ...horarioEditado,
                      capacidadMaxima: e.target.value,
                    })
                  }
                />
              </div>
            </form>
            console.log(${horario.idHorario})
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
              onClick={editarHorario}
              data-bs-dismiss="modal"
            >
              Guardar cambios
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalEditarHorario;
