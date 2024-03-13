import React from "react";
import { Outlet, Link } from "react-router-dom";
import { useEffect, useState } from "react";
import "./index.css";
import { capitalizeWords } from "../utilities/utils";

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
        <Link to="/">
          <img
            src="/heartlogo.png"
            alt="logo"
            className="image"
            onClick={() => setPath("/")}
          />
        </Link>
        <h1 className="heading">Welcome to the Health Manager App!</h1>
      </div>
      <div className="Layout">
        <nav>
          {user === null ? (
            <>
              <Link to="/register">
                <button type="button" onClick={() => setPath("/register")}>
                  Register
                </button>
              </Link>

              <Link to="/login">
                <button type="button" onClick={() => setPath("/login")}>
                  Login
                </button>
              </Link>
            </>
          ) : user !== "admin" ? (
            <>
              <Link to="/profile">
                <button type="button" onClick={() => setPath("/profile")}>
                  Profile
                </button>
              </Link>
            </>
          ) : (
            <>
              <Link to="/admin">
                <button type="button" onClick={() => setPath("/admin")}>
                  Admin site
                </button>
              </Link>
            </>
          )}
          <Link to="/drinks">
            <button type="button" onClick={() => setPath("/drinks")}>
              Search drink
            </button>
          </Link>

          <Link to="/food">
            <button type="button" onClick={() => setPath("/food")}>
              Search food
            </button>
          </Link>

          <Link to="/activities">
            <button type="button" onClick={() => setPath("/activities")}>
              Search activities
            </button>
          </Link>
        </nav>
        <Outlet />
      </div>
    </div>
  );
}

export default App;
