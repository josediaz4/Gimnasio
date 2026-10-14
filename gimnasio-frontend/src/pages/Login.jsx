import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

function Login() {
  const { login } = useContext(AuthContext); //acceso al login del contexto
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch("https://localhost:7274/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
        throw new Error("Credenciales invalidas.");
      }

      const data = await response.json();
      //decodificar el token jwt
      const decoded = jwtDecode(data.token);
      //guardar en el contxto: token + datos del payload
      login({
        token: data.token,
        name: decoded.name,
        role: decoded.role,
      });
      console.log("Login exitoso: ", data);
      navigate("/dashboard");
    } catch (error) {
      console.error("Error en el login: ", error.message);
      alert("Usuario o contraseña incorrectos.");
    }

    console.log("Email: ", email);
    console.log("Password: ", password);

    //**Mas adelante aqui llamamos a la API para validar login
  };

  return (
    <div className="container mt-5">
      <h2 className="text-center mb-4">Iniciar Sesión</h2>
      <form
        onSubmit={handleSubmit}
        className="mx-auto"
        style={{ maxWidth: "400px" }}
      >
        <div className="mb-3">
          <label className="form-label">Correo electronico</label>
          <input
            type="email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="ejemplo@correo.com"
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Contraseña</label>
          <input
            type="password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="******"
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">
          Entrar
        </button>
      </form>
    </div>
  );
}

export default Login;
