import React, { useState } from "react";
import { Outlet, Link } from "react-router-dom";
import "./App.css";


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src="/heartlogo.png" alt="logo" width="70%" height="auto" />
        <h1>Welcome to the Health Manager App!</h1>
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
          </nav>
          <Outlet />
        </div>
      </header>
    </div>
  );
}

export default App;
