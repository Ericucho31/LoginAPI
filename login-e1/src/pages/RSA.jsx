import React from 'react';
import LoginForm from '../componentes/Login';
import CrearUsuario from '../componentes/CrearUsuario';

const RSA = () => {
  return (
    <div className='flex justify-center items-center h-screen' >
      <header>
        <p>Iniciar Sesion</p>
        <LoginForm></LoginForm>
        <p>Crear cuenta</p>
        <CrearUsuario></CrearUsuario>
        
      </header>
    </div>
  );
};

export default RSA;
