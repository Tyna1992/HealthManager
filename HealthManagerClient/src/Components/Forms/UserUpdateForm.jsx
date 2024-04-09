import React, { useState } from 'react';
import "../../index.css";

function UserUpdateForm({ data, onUpdate, clearData }) {
    const [formData, setFormData] = useState({
        id: data.id,
        email: data.email,
        userName: data.userName,
        gender: data.gender || "",
        weight: data.weight || 0,
        phoneNumber: data.phoneNumber || ""
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    }

    const handleUpdateClick = () => {
        onUpdate(data.id, formData);
        clearData();
    }

    return (
        <div className="form-container">
            <h2>Update User</h2>
            <br />
            <label>Id:</label>
            <br />
            <input type="text" name="id" value={formData.id} disabled={true} />
            <br />
            <label>Email:</label>
            <br />
            <input type="text" name="email" value={formData.email} disabled={true} />
            <br />
            <label>Username:</label>
            <br />
            <input type="text" name="userName" value={formData.userName} onChange={handleChange} />
            <br />
            <label>Gender:</label>
            <br />
            <input type="text" name="gender" value={formData.gender} onChange={handleChange} />
            <br />
            <label>Weight:</label>
            <br />
            <input type="number" name="weight" value={formData.weight} onChange={handleChange} />
            <br />
            <label>Phone Number:</label>
            <br />
            <input type="text" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} />
            <br />
            <button onClick={handleUpdateClick}>Update</button>
            <button onClick={clearData}>Cancel</button>
        </div>
    );
}

export default UserUpdateForm;