import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { Link } from "react-router-dom";

function Navbar() {
  const { user, logout } = useContext(AuthContext);

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container-fluid">
        <Link className="navbar-brand" to="/dashboard">
          PitbullGym
        </Link>

        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-targer="#navbarNav"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav me-auto">
            {user?.role === "Administrador" && (
              <>
                <li className="nav-item">
                  <Link className="nav-link" to="/socios">
                    Socios
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/pagos">
                    Pagos
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/horarios">
                    Horarios
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/planes">
                    Planes
                  </Link>
                </li>
              </>
            )}
            {user?.role === "Socio" && (
              <li className="nav-item">
                <Link className="nav-link" to="/perfil">
                  Mi Perfil
                </Link>
              </li>
            )}
          </ul>

          <span className="navbar-text me-3">
            {user && `Hola ${user.name}`}
          </span>
          <button className="btn btn-outline-light" onClick={logout}>
            Cerrar Sesión
          </button>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
