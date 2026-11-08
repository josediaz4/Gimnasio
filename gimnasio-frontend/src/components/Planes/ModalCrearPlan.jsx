import { useState } from "react";

function ModalCrearPlan({ onPlanCreado }) {
  const API_URL = "https://localhost:7274/api/PlanesPago";

  const [nuevoPlan, setNuevoPlan] = useState({
    nombrePlan: "",
    precio: 0,
    cantidadDias: 0,
  });

  const crearPlan = async () => {
    //Validaciones
    if (!nuevoPlan.nombrePlan) {
      alert("El campo es obligatorio");
      return;
    }
    if (!nuevoPlan.precio < 0) {
      alert("Ingresar un precio valido");
      return;
    }
    if (nuevoPlan.cantidadDias < 0) {
      alert("Cantidad de dias debe ser mayor a 0");
      return;
    }
    //POST a la API
    await fetch(`${API_URL}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(nuevoPlan),
    });

    //Limpiar formulario
    setNuevoPlan({
      nombrePlan: "",
      precio: 0,
      cantidadDias: 0,
    });

    //Avisar al padre que se creo un nuevo horario
    onPlanCreado();
  };

  return (
    <div
      className="modal fade"
      id="modalCrearPlan"
      tabIndex="-1"
      aria-hidden="true"
    >
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Crear Nuevo Plan</h5>
            <button
              type="button"
              className="btn-close"
              data-bs-dismiss="modal"
            ></button>
          </div>
          <div className="modal-body">
            <form>
              <div className="mb-3">
                <label className="form-label">Nombre del plan</label>
                <input
                  type="text"
                  className="form-control"
                  value={nuevoPlan.nombrePlan}
                  onChange={(e) =>
                    setNuevoPlan({
                      ...nuevoPlan,
                      nombrePlan: e.target.value,
                    })
                  }
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Precio</label>
                <div className="input-group">
                  <span className="input-group-text">$</span>
                  <input
                    type="number"
                    className="form-control"
                    step={1000}
                    min={0}
                    value={nuevoPlan.precio}
                    onChange={(e) =>
                      setNuevoPlan({
                        ...nuevoPlan,
                        precio: e.target.value,
                      })
                    }
                  />
                </div>
              </div>
              <div className="mb-3">
                <label className="form-label">Cantidad de dias</label>
                <div className="input-group">
                  <span className="input-group-text">$</span>
                  <input
                    type="number"
                    className="form-control"
                    step={1}
                    min={0}
                    value={nuevoPlan.cantidadDias}
                    onChange={(e) =>
                      setNuevoPlan({
                        ...nuevoPlan,
                        cantidadDias: e.target.value,
                      })
                    }
                  />
                </div>
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
              onClick={crearPlan}
            >
              Guardar
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ModalCrearPlan;
