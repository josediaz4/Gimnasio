export const apiFetch = async (url, options = {}, token) => {
  const headers = {
    ...options.headers,
    Authorization: `Bearer ${token}`, //enviamos el token
    "Content-Type": "application/json",
  };

  const response = await fetch(url, { ...options, headers });

  if (response.status === 401) throw new Error("Unauthorized");
  if (!response.ok) throw new Error("Error en la petición");
  return response.json();
};
