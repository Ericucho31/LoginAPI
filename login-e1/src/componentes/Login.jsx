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
        'privateKey': privateKey
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
    <form class="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 flow-root" onSubmit={handleSubmit}>
      {error && <div class="text-red-700 font-bold text-sm" >{error}</div>}
      {confirmacion && <div class="text-blue-700 font-bold text-md">{confirmacion}</div>}
      {fiesta && <div>{fiesta}</div>}
      <div class="w-full max-w-xs ">
        <input type="text" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" placeholder="Correo Electrónico" value={email} onChange={(e) => setEmail(e.target.value)} />
        <input type="password" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" placeholder="Contraseña" value={password} onChange={(e) => setPassword(e.target.value)} />
        <input type="number" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4" placeholder="Llave Privada" value={privateKey} onChange={(e) => setPrivateKey(e.target.value)} />

        <button type="submit" class="text-white-700 font-bold bg-blue-500 border rounded py-2 px-4 ">Iniciar sesión</button>
      </div>
    </form>
  );
}

export default LoginForm;
