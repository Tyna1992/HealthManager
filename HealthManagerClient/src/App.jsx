import React, { useState } from "react";
import { Outlet, Link } from "react-router-dom";
import "./App.css";
import RegisterUser from "../components/Register.jsx";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src="/heartlogo.png" alt="logo" width="70%" height="auto" />
        <h1>Welcome to the Health Manager App!</h1>
        <div className="Layout">
          <nav>
            <ul>
              <li>
                <Link to="/register">
                  <button type="button">Register</button>
                </Link>
              </li>
              <li>
                <Link to="/login">
                  <button type="button">Login</button>
                </Link>
              </li>
              <li>
                <Link to="/drinks">
                  <button type="button">Search drink</button>
                </Link>
              </li>
              <li>
                <Link to="/food">
                  <button type="button">Search food</button>
                </Link>
              </li>
            </ul>
          </nav>
          <Outlet />
        </div>
      </header>
    </div>
  );
}

export default App;
