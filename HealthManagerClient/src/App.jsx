import React from "react";
import { Outlet, Link } from "react-router-dom";
import { useEffect, useState } from "react";
import "./index.css";

function App() {
  const [user, setUser] = useState(null);
  const [email, setEmail] = useState(null);
  const [path, setPath] = useState("");

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await fetch("/api/Auth/WhoAmI", {
          method: "GET",
          credentials: "include",
          headers: {
            "Content-Type": "application/json",
          },
        });
        const data = await response.json();
        
        if (data) {
          setUser(data.userName);
          setEmail(data.email);
        }
      } catch (error) {
        console.log("Error", error);
      }
    }
    fetchData();
  }, [path]);

  return (
    <div className="App">
      <div className="container">
        <img src="/heartlogo.png" alt="logo" className="image" />
        <h1 className="heading">Welcome to the Health Manager App!</h1>
      </div>
      <div className="Layout">
        <nav>
          <Link to="/">
            <button type="button" onClick={() => setPath("/")}>Home</button>
          </Link>
          {user === null ? (
            <>
              <Link to="/register">
                <button type="button">Register</button>
              </Link>

              <Link to="/login">
                <button type="button">Login</button>
              </Link>
            </>
          ) : (
            <div>
              <button type="button">Welcome {user}</button>
            </div>
          )}

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
