import React, {useEffect, useState} from 'react';
import {useLocation} from "react-router-dom";
import MealPlanTableComponent from '../Components/Tables/MealPlanTableComponent';

function Profile() {
    const [userData, setUserData] = useState({});   
    const [userMealPlan, setUserMealPlan] = useState([]);

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

    const getMealPlans = async () =>{
        try {
            const response = await fetch(`/api/mealPlan/getByUserName/${userData.userName}`, {
                method: "GET",
                credentials: "include",
                headres:{
                    "Content-Type": "application/json"
                }
            });
            const data = await response.json();
            setUserMealPlan(data);
        } 
        catch (error) {
            console.log("Error", error);
        }
    }
    
    return (
        <div className="profile">
            <h1>Welcome {userData.userName}!</h1>
            <h2>Profile</h2>
            <p>Email: {userData.email}</p>
            <p>Username: {userData.userName}</p>
            <p>Gender: {userData.gender}</p>
            <p>Weight: {userData.weight} kg</p>
            <button>Edit</button>
            <button>Delete</button>
            <button onClick={() => getMealPlans()}>Show diet plan</button>
            {userMealPlan.length !== 0 ?
            <div>
                <h2>Diet Plans</h2>
                <MealPlanTableComponent dataArray={userMealPlan} />
            </div>
            :
            ""
            }
        </div>
    );
};

export default Profile;