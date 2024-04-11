import React, {useEffect, useState} from 'react';
import MealPlanTableComponent from '../Components/Tables/MealPlanTableComponent';

function Profile() {
    const [userData, setUserData] = useState({});   
    const [userMealPlan, setUserMealPlan] = useState([]);
    const [loading, setLoading] = useState(false);

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
            setLoading(true);
            const response = await fetch(`/api/mealPlan/getByUserName/${userData.userName}`, {
                method: "GET",
                credentials: "include",
                headres:{
                    "Content-Type": "application/json"
                }
            });
            const data = await response.json();
            setUserMealPlan(data);
            setLoading(false);
        } 
        catch (error) {
            console.log("Error", error);
        }
    }

    const deleteMealPlan = async (id) => {
        try {
            const response = await fetch(`/api/mealPlan/delete/${id}`, {
                method: "DELETE",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
            });
            if (response.ok) {
                const newMealPlan = userMealPlan.filter((mealPlan) => mealPlan.id !== id);
                setUserMealPlan(newMealPlan);
            }
        } catch (error) {
            console.log("Error", error);
        }
    }
    
    return (
        <div className="profile">
            <h1>Welcome {userData.userName}!</h1>
            <h2>Profile</h2>
            <table>
                <tbody>
                <tr>
                    <th>Email</th>
                    <th>User name</th>
                    <th>Gender</th>
                    <th>Weight</th>
                </tr>
                <tr>
                    <td>{userData.email}</td>
                    <td>{userData.userName}</td>
                    <td>{userData.gender}</td>
                    <td>{userData.weight} kg</td>
                </tr>
                </tbody>
            </table>
            <button disabled>Edit</button>
            <button disabled>Delete</button>
            <button onClick={() => getMealPlans()}>Show diet plan</button>
            {userMealPlan.length !== 0 && !loading?
            <div>
                <h2>Diet Plans</h2>
                <MealPlanTableComponent dataArray={userMealPlan} onDelete={deleteMealPlan} />
            </div>
            : loading ? <h1>Loading Meal Plans...</h1> : null
            }
        </div>
    );
};

export default Profile;