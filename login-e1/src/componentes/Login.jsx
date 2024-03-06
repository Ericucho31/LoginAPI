import React, { Component, useState } from 'react';
import axios from 'axios';
import FiestaLoca from './FiestaLoca';


function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [privateKey, setPrivateKey] = useState();

  const [error, setError] = useState(null);
  const [confirmacion, setConfirmacion] = useState(null);
  const [fiesta, setFiesta] = useState();

  const handleSubmit = async (e) => {
    setError(' ');
    setConfirmacion(' ');
    e.preventDefault();
    
    // Validación de datos
    if (!email || !password) {
      setError('Por favor, complete todos los campos.');
      return;
    }
    
    // Aquí puedes enviar los datos al servidor para autenticación
    try {
      const response = await axios.post('https://localhost:7145/api/Usuario/Login', {
        'email': email,
        'password': password,
        'privateKey':privateKey
      });
        setConfirmacion('Excelente, si eres tu.');
        setFiesta(<FiestaLoca></FiestaLoca>)
        console.log(response)
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
    <form onSubmit={handleSubmit}>
      {error && <div>{error}</div>}
      {confirmacion && <div>{confirmacion}</div>}
      {fiesta && <div>{fiesta}</div>}
      <input type="text" placeholder="Correo Electrónico" value={email} onChange={(e) => setEmail(e.target.value)} />
      <input type="password" placeholder="Contraseña" value={password} onChange={(e) => setPassword(e.target.value)} />
      <input type="number" placeholder="Llave Privada" value={privateKey} onChange={(e) => setPrivateKey(e.target.value)} />
      
      <button type="submit">Iniciar sesión</button>
      
    </form>
  );
}

export default LoginForm;
