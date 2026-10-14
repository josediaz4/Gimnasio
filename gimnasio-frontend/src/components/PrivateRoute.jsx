import { useContext } from "react";
import { Navigate } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

const PrivateRoute = ({ children, roles }) => {
  const { user } = useContext(AuthContext); //usamo el contexto global

  //si no hay usuarios, redirige al login
  if (!user) return <Navigate to="/login" />;
  //si se especifican roles, y el rol del usuario no esta permitido
  //redirige al dashboard
  if (roles && !roles.includes(user.role)) return <Navigate to="/dashboard" />;

  //si todo esta ok, renderizo al componente hijo
  return children;
};

export default PrivateRoute;
