import React, { useState } from 'react';

const SQL = () => {
  const [searchQuery, setSearchQuery] = useState('');

  const handleSearch = () => {
    // Simulación de una consulta SQL vulnerable a SQL injection
    const sqlQuery = `SELECT * FROM products WHERE name='${searchQuery}'`;

    // Aquí podrías hacer algo con la consulta, como enviarla al backend para ejecución o mostrarla al usuario
    console.log('Consulta SQL generada:', sqlQuery);
  };

  return (
    <div>
      <h2 className="text-xl font-semibold mb-2">Buscador vulnerable a SQL Injection</h2>
      <input
        type="text"
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        placeholder="Ingrese su consulta de búsqueda..."
        className="border border-gray-300 rounded-md p-2"
      />
      <button
        onClick={handleSearch}
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mt-2"
      >
        Buscar
      </button>
    </div>
  );
};

export default SQL;