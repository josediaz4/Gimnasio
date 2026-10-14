function Pagos() {
  return (
    <div className="container mt-4">
      <h2 className="mb-3">Gestion de Pagos</h2>
      <div className="card shadow">
        <div className="card-body">
          <p className="card-text">
            Controla las cuotas y pagos de los socios.
          </p>
          <table className="table table-hover mt-3">
            <thead>
              <tr>
                <th>Nombre del Socio</th>
                <th>Plan</th>
                <th>Monto</th>
                <th>Fecha del Pago</th>
                <th>Metodo de Pago</th>
                <th>Estado</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>María López</td>
                <td>Mensual</td>
                <td>$23.000</td>
                <td>10/08/2026</td>
                <td>Transferencia</td>
                <td>
                  <span className="badge bg-success">Pagado</span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Pagos;
