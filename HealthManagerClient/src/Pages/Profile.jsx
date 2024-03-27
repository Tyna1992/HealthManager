import React, {useEffect, useState} from 'react';
import {useLocation} from "react-router-dom";

function Profile() {

    const [user, setUser] = useState(null);
    const [email, setEmail] = useState(null);
    

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
    }, []);
    
    return (
        <div className="profile">
            <h1>User Profile</h1>
            <br/>
            <h2>Username:</h2>
            <h3>{user}</h3>
            <br/>
            <h2>Email:</h2>
            
            <h3>{email}</h3>


        </div>
    );
};

export default Profile;