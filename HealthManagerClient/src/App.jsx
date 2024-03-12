import React from "react";
import { Outlet, Link } from "react-router-dom";
import "./index.css";


function App() {
  return (
    <div className="App">
      <div className="container">
        <img src="/heartlogo.png" alt="logo" className="image" />
        <h1 className="heading">Welcome to the Health Manager App!</h1>
        </div>
        <div className="Layout">
          <nav>
            <Link to="/">
              <button type="button">Home</button>
            </Link>

            <Link to="/register">
              <button type="button">Register</button>
            </Link>
            
            <Link to="/login">
              <button type="button">Login</button>
            </Link>

            <Link to="/drinks">
              <button type="button">Search drink</button>
            </Link>

            <Link to="/food">
              <button type="button">Search food</button>
            </Link>
            <Link to="/activities">
              <button type="button">Search activities</button>
            </Link>
          </nav>
          <Outlet />
        </div>
    </div>
  );
}

export default App;
