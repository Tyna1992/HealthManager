import React, {useEffect, useState} from 'react';
import {useLocation} from "react-router-dom";

function Profile() {
    const [userData, setUserData] = useState({});    

    useEffect(() => {
        async function fetchData() {
            let email = "";
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
                    email = data.email;
                }
                const response2 = await fetch(`/api/User/getByEmail/${email}`, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    },
                });
                const data2 = await response2.json();
                console.log(data2);
                if (data2) {
                    setUserData(data2);
                }
            } catch (error) {
                console.log("Error", error);
            }
        }
        fetchData();
    }, []);
    
    return (
        <div className="profile">
            <h1>Welcome {userData.userName}!</h1>
            <h2>Profile</h2>
            <p>Email: {userData.email}</p>
            <p>Username: {userData.userName}</p>
            <p>Gender: {userData.gender}</p>
            <p>Weight: {userData.weight}</p>
            <p>Phone Number: {userData.phoneNumber}</p>
            <button>Edit</button>
            <button>Delete</button>
        </div>
    );
};

export default Profile;