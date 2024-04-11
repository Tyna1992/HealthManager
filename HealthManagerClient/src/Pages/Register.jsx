import React from "react";
import { useNavigate } from "react-router-dom";
import "../index.css";

function RegisterUser() {
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
            const response = await fetch("/api/Auth/Register",{
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
                <label>Username:</label>
                <br></br>
                <input type="text" name="username"></input>
                <br></br>
                <label>Email:</label>
                <br></br>
                <input type="email" name="email"></input>
                <br></br>
                <label>Password:</label>
                <br></br>
                <input type="password" name="password"></input>
                <br></br>
                <label>Weight in kg:</label>
                <br></br>
                <input type="number"name="weight"></input>
                <br></br>
                <label>Gender:</label>
                <br></br>
                <select name="gender">
                    <option disabled>Select please</option>
                    <option value={"female"}>Female</option>
                    <option value={"male"}>Male</option>
                    <option value={"other"}>Other</option>
                </select>
                <br></br>
                <button type="submit">Register</button>
                <button type="button" onClick={() => navigate("/")}>Cancel</button>
            </form>
        </div>
    )
}

export default RegisterUser;