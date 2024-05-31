import React, { useState } from 'react';

const SQL = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);

  const handleSearch = () => {
    // Simulación de inyección SQL (no lo hagas en un entorno de producción)
    const query = `SELECT * FROM Users WHERE username LIKE '%${searchTerm}%';`;

    // Ejecutar la "consulta" (simulada)
    // ¡No ejecutes consultas SQL reales de esta manera en una aplicación real!
    // Esta es una simulación con fines educativos solamente.
    fetch(`/api/search?query=${encodeURIComponent(query)}`)
      .then(response => response.json())
      .then(data => {
        setSearchResults(data);
      })
      .catch(error => {
        console.error('Error al ejecutar la consulta:', error);
      });
  };

  return (
    <div>
      <h2>Simulador de Inyección SQL (Solo con fines educativos)</h2>
      <input
        type="text"
        value={searchTerm}
        onChange={e => setSearchTerm(e.target.value)}
        placeholder="Buscar usuarios"
      />
      <button onClick={handleSearch}>Buscar</button>
      
      <h3>Resultados de la búsqueda:</h3>
      <ul>
        {searchResults.map(user => (
          <li key={user.id}>{user.username}</li>
        ))}
      </ul>
    </div>
  );
};

export default SQL;
