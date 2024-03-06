import logo from './logo.svg';
import './App.css';
import LoginForm from './componentes/Login';
import CrearUsuario from './componentes/CrearUsuario';
import LoginBonito from './componentes/LoginBonito';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p>Iniciar Sesion</p>
        <LoginForm></LoginForm>
        <p>Crear cuenta</p>
        <CrearUsuario></CrearUsuario>

        <LoginBonito></LoginBonito>
        
      </header>
    </div>
  );
}

export default App;
