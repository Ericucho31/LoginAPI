import logo from './logo.svg';
import './App.css';

import RSA from './pages/RSA';
import XSS from './pages/XSS';
import SQL from './pages/SQLinjection';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Phishing from './pages/Phishing';

function App() {
  return (
    <Router>
      <div className="App">
        <nav className="bg-blue-600 p-4">
          <ul className="flex space-x-4">
            <li>
              <Link to="/" className="text-white hover:bg-blue-700 px-3 py-2 rounded">RSA</Link>
            </li>
            <li>
              <Link to="/xss" className="text-white hover:bg-blue-700 px-3 py-2 rounded">XSS</Link>
            </li>
            <li>
              <Link to="/sql" className="text-white hover:bg-blue-700 px-3 py-2 rounded">SQL</Link>
            </li>
            <li>
              <Link to="/phishing" className="text-white hover:bg-blue-700 px-3 py-2 rounded">Phishing</Link>
            </li>

          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<RSA />} />
          <Route path="/xss" element={<XSS />} />
          <Route path="/sql" element={<SQL />} />
          <Route path="/phishing" element={<Phishing />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
