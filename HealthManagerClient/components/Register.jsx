import React from "react";
import { useNavigate } from "react-router-dom";

function RegisterUser(props) {
    const navigate = useNavigate()

    async function handleSubmit(event) {
        event.preventDefault();
        const userName = event.target.username.value;
        const email = event.target.email.value;
        const password = event.target.password.value;
        const weight = parseFloat(event.target.weight.value);
        const gender = event.target.gender.value;
        const user = {userName, email, password,weight, gender};
        try{
            const response = await fetch("http://localhost:5179/api/user/register",{
                method: "POST",
                headers:{
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(user)
            })
            console.log(user)
            alert("Registration successful!");
            event.target.username.value = "";
            event.target.email.value = "";
            event.target.password.value = "";
            event.target.weight.value = "";
            event.target.gender.value = "";
        } catch(e) {
            alert("Registration failed!");
            console.log(e);
        }
    }

    return (
        <div className="registration">
            <form className="registrationForm" onSubmit={handleSubmit} >
                <label>
                    Username:
                </label>
                <input type="text"
                    name="username"
                >
                </input>
                <label>Email:</label>
                <input
                    type="email"
                    name="email"
                ></input>
                <label>Password:</label>
                <input
                    type="password"
                    name="password"
                ></input>
                <label>Weight:</label>
                <input
                    type="number"
                    name="weight"
                ></input>
                <label>Gender:</label>
                <select name="gender">
                    <option disabled>Select please</option>
                    <option value={"female"}>Female</option>
                    <option value={"male"}>Male</option>
                    <option value={"other"}>Other</option>
                </select>
                <button type="submit">Register</button>
            </form>
            <button type="button" onClick={() => navigate("/")}>Cancel</button>
        </div>
    )
}

export default RegisterUser;