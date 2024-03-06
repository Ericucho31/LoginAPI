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

  

  const iniciarSesion = ()=> {
    

  };

  return (
    <form onSubmit={handleSubmit}>
      {error && <div>{error}</div>}
      {confirmacion && <div>{confirmacion}</div>}
      <input type="text" placeholder="Correo Electrónico" value={email} onChange={(e) => setEmail(e.target.value)} />
      <input type="password" placeholder="Contraseña" value={password} onChange={(e) => setPassword(e.target.value)} />
      <button type="submit">Iniciar sesión</button>
    </form>
  );
}

export default CrearUsuario;