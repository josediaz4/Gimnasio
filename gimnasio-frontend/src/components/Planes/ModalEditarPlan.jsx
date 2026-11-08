import { placements } from "@popperjs/core";
import { useState } from "react";

function ModalEditarPlan({ plan, onPlanActualizado }) {
  const API_URL = "https://localhost:7274/api/PlanesPago";

  const [planEditado, setPlanEditado] = useState(plan);

  const editarPlan = async () => {
    if (!planEditado.nombrePlan) {
      alert("El campo es obligatorio");
      return;
    }
    if (!planEditado.precio < 0) {
      alert("Ingresar un precio valido");
      return;
    }
    if (planEditado.cantidadDias < 0) {
      alert("Cantidad de dias debe ser mayor a 0");
      return;
    }

    await fetch(`${API_URL}/${planEditado.idPlan}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(planEditado),
    });

    //Avisar al padre que se actualizó
    onPlanActualizado();
  };

  return (
    <div className="modal fade" id={`modalEditar-${plan.idPlan}`} tabIndex="-1">
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Editar Plan</h5>
            <button
              type="button"
              className="btn-close"
              data-bs-dismiss="modal"
            ></button>
          </div>
          <div className="modal-body">
            <form>
              <div className="mb-3">
                <label className="form-label">Nombre Plan</label>
                <input
                  type="text"
                  className="form-control"
                  value={planEditado.nombrePlan}
                  onChange={(e) =>
                    setPlanEditado({
                      ...planEditado,
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
                    value={planEditado.precio}
                    onChange={(e) =>
                      setPlanEditado({
                        ...planEditado,
                        precio: e.target.value,
                      })
                    }
                  />
                </div>
              </div>
              <div className="mb-3">
                <label className="form-label">Cantidad de dias</label>
                <input
                  type="number"
                  className="form-control"
                  value={planEditado.cantidadDias}
                  onChange={(e) =>
                    setPlanEditado({
                      ...planEditado,
                      cantidadDias: e.target.value,
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
              onClick={editarPlan}
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

export default ModalEditarPlan;
