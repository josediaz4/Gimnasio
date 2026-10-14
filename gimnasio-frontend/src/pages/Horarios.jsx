function Horarios() {
  return (
    <div className="container mt-4">
      <h2 className="mb-3">Gestion de Horarios</h2>
      <div className="card shadow">
        <div className="card-body">
          <p className="card-text">
            Administra las clases y turnos disponibles.
          </p>
          <ul className="list-group mt-3">
            <li className="list-group-item">Lunes 18:00 - Crossfit</li>
            <li className="list-group-item">Martes 19:00 - Spinning</li>
            <li className="list-group-item">Miércoles 20:00 - Yoga</li>
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Horarios;
