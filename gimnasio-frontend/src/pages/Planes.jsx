function Planes() {
  return (
    <div className="container mt-4">
      <h2 className="mb-3">📦 Gestión de Planes</h2>
      <div className="card shadow">
        <div className="card-body">
          <p className="card-text">
            Define los planes y membresías disponibles para los socios.
          </p>
          <div className="row mt-3">
            <div className="col-md-4">
              <div className="card text-center">
                <div className="card-body">
                  <h5 className="card-title">Plan Básico</h5>
                  <p className="card-text">$3000 / mes</p>
                  <button className="btn btn-primary">Editar</button>
                </div>
              </div>
            </div>
            <div className="col-md-4">
              <div className="card text-center">
                <div className="card-body">
                  <h5 className="card-title">Plan Premium</h5>
                  <p className="card-text">$6000 / mes</p>
                  <button className="btn btn-primary">Editar</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Planes;
