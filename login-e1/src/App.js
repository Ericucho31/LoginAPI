import logo from './logo.svg';
import './App.css';
import LoginForm from './componentes/Login';
import CrearUsuario from './componentes/CrearUsuario';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p>Iniciar Sesion</p>
        <LoginForm></LoginForm>
        <p>Crear cuenta</p>
        <CrearUsuario></CrearUsuario>
        
      </header>
    </div>
  );
}

export default App;
