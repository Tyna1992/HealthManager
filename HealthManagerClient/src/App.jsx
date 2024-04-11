import React from "react";
import { Outlet, Link, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import "./index.css";
import LogoutButton from "./Components/LogoutButton";

function App() {
  const [user, setUser] = useState(null);
  const [theme, setTheme] = useState(localStorage.getItem("theme") || "light");
  const location = useLocation();

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
        }
      } catch (error) {
        console.error(error);
      }
    }
    fetchData();
  }, [location.pathname]);

  function changeTheme() {
    const newTheme = theme === "light" ? "dark" : "light";
    setTheme(newTheme);
    localStorage.setItem("theme", newTheme);
  }

  useEffect(() => {
    document.body.className = theme;
  }, [theme]);

  return (
    <div className="App">
      <div className="container">
        <Link to="/">
          <img src="/heartlogo.png" alt="logo" className="image" />
        </Link>
        <h1 className="heading">Welcome to the Health Manager App!</h1>
      </div>
      <div className="Layout">
        <nav>
          {user === null ? (
            <>
              <Link to="/register">
                <button type="button">Register</button>
              </Link>
              <Link to="/login">
                <button type="button">Login</button>
              </Link>
            </>
          ) : user !== "admin" ? (
            <>
              <Link to="/profile">
                <button type="button">Profile</button>
              </Link>
              <LogoutButton />
              <Link to="/mealplanner">
                <button type="button"> Diet planner </button>
              </Link>
            </>
          ) : (
            <>
              <Link to="/admin">
                <button type="button">Admin site</button>
              </Link>
              <LogoutButton />
            </>
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
          <button type="button" onClick={changeTheme}>
            {theme === "light" ? "Dark mode" : "Light mode"}
          </button>
        </nav>
        <Outlet />
      </div>
    </div>
  );
}

export default App;