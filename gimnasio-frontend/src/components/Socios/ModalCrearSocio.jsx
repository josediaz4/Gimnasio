import { useState } from "react";
import { metodosPago } from "../../utils/metodosPago";

function ModalCrearSocio({ onSocioCreado }) {
  const API_URL = "https://localhost:7274/api/socios";
  const api_planes = "https://localhost:7274/api/PlanesPago";

  const [nuevoSocio, setNuevoSocio] = useState({
    nombreCompleto: "",
    email: "",
    dni: "",
    metodoPago: "",
    estadoMembresia: true,
  });

  const crearSocio = async () => {
    //Validaciones
    if (!nuevoSocio.nombreCompleto) {
      alert("El campo es obligatorio");
      return;
    }
    if (!nuevoSocio.email) {
      alert("El campo es obligatorio");
      return;
    }
    if (nuevoSocio.dni) {
      alert("El campo es obligatorio");
      return;
    }
    nuevoSocio.estadoMembresia = true;
    //POST a la API
    await fetch(`${API_URL}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(nuevoSocio),
    });

    //Limpiar formulario
    nuevoSocio({
      nombreCompleto: "",
      email: "",
      dni: "",
      metodoPago: "",
      estadoMembresia: true,
    });

    //Avisar al padre que se creo un nuevo horario
    onSocioCreado();
  };

  return (
    <div
      className="modal fade"
      id="modalCrearSocio"
      tabIndex="-1"
      aria-hidden="true"
    >
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Crear Nuevo Socio</h5>
            <button
              type="button"
              className="btn-close"
              data-bs-dismiss="modal"
            ></button>
          </div>
          <div className="modal-body">
            <form>
              <div className="mb-3">
                <label className="form-label">Nombre Completo</label>
                <input
                  type="text"
                  className="form-control"
                  value={nuevoSocio.nombreCompleto}
                  onChange={(e) =>
                    setNuevoSocio({
                      ...nuevoSocio,
                      nombreCompleto: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Correo</label>
                <input
                  type="text"
                  className="form-control"
                  value={nuevoSocio.email}
                  onChange={(e) =>
                    setNuevoSocio({
                      ...nuevoSocio,
                      email: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">DNI</label>
                <input
                  type="text"
                  className="form-control"
                  value={nuevoSocio.dni}
                  onChange={(e) =>
                    setNuevoSocio({
                      ...nuevoSocio,
                      dni: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Método de Pago</label>
                <select
                  className="form-select"
                  value={nuevoSocio.metodoPago}
                  onChange={(e) =>
                    setNuevoSocio({
                      ...nuevoSocio,
                      metodoPago: e.target.value,
                    })
                  }
                  required
                >
                  <option value="">Seleccionar</option>
                  {metodosPago.map((forma, index) => (
                    <option key={index} value={forma}>
                      {forma}
                    </option>
                  ))}
                </select>
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
              onClick={crearSocio}
            >
              Guardar
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalCrearSocio;
