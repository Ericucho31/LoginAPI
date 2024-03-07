import React, { useState } from 'react';
import axios from 'axios';

function CrearUsuario() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);
  const [confirmacion, setConfirmacion] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    // Validación de datos
    if (!email || !password) {
      setError('Por favor, complete todos los campos.');
      return;
    }
    
    try {
      const response = await axios.post('https://localhost:7145/api/Usuario/CrearUsuarioAsimetrico', {
        email: email,
        password: password,
      });
      setConfirmacion('Se creó el usuario ' + email);
      console.log(response);
    } catch (error) {

      if (error.response && error.response.status === 400) {
        setError(error.response.data);
      } else {
        setError('Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo de nuevo más tarde.');
      }
      console.log(error);
    }
  };

  return (
    <form class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4" onSubmit={handleSubmit}>
      {error && <div class="text-red-700 font-bold text-sm" >{error}</div>}
      {confirmacion && <div class="text-blue-700 font-bold text-md">{confirmacion}</div>}
      <div class="w-full max-w-xs">
        <input type="text" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" placeholder="Correo Electrónico" value={email} onChange={(e) => setEmail(e.target.value)} />
        <input type="password" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" placeholder="Contraseña" value={password} onChange={(e) => setPassword(e.target.value)} />
      
        <button type="submit" class="text-white-700 font-bold bg-blue-500 border rounded py-2 px-4 ">Crear Usuario</button>
      </div>
    </form>


  );
}

export default CrearUsuario;