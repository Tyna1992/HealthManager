import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App";
import Register from "./Pages/Register";
import Meals from "./Pages/Meals";
import Cocktails from "./Pages/Cocktails";
import Activities from "./Pages/Activities";
import Login from "./Pages/Login";


const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children:[
            {
                path: "/register",
                element: <Register/>
            },
            {
                path: "/food",
                element: <Meals/>
            },
            {
                path: "/drinks",
                element: <Cocktails/>
            },
            {
                path: "/activities",
                element: <Activities/>
            },
            {
                path: "/login",
                element: <Login/>
            }
        ]
    }
]
);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
)