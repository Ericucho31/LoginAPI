import axios from 'axios';
import React, { useEffect, useState } from 'react';

const SQL = () => {

    const [link, setLink] = useState('')

    useEffect(() => {
        const apiKey = 'c6a11324-7324-47a2-87c2-dbf6e058b3fc';
        const email = 'kyshon.stan@dockleafs.com'
        const options = { method: 'GET', headers: { accept: 'application/json' } };
    
        // Define la función de búsqueda dentro del useEffect
        const fetchingLink = async () => {
          try {
            const response = await axios.get(`https://caniphish.com/ManagementAPI/GetPhishingWebsites?emailAddress=${email}&apiKey=${apiKey}`);
            const data = await response.json();
            setLink(data);
          } catch (error) {
            console.error(error);
          }
        };
    
        // Llama a la función fetchingLink dentro del useEffect
        fetchingLink();
    
        // Asegúrate de añadir apiKey y options como dependencias si son necesarias.
      }, []); // Las dependencias están vacías para que solo se ejecute una vez al montar el componente.
    

    return (
        <div>
            <iframe
                title="Contenido externo"
                src={link}
                width="1529"
                height="600"
                frameBorder="0"
            />

        </div>
    );
};

export default SQL;