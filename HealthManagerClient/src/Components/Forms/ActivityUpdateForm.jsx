import React, { useState } from 'react';
import "../../index.css";

function ActivityUpdateForm({ data, onUpdate, clearData }) {
    const [formData, setFormData] = useState({
        id: data.id,
        name: data.name,
        calories_per_hour: data.calories_per_hour,
        weight: data.weight,
        duration_minutes: data.duration_minutes,
        total_calories: data.total_calories
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
            <h2>Update Activity</h2>
            <br />
            <label>Id:</label>
            <br />
            <input type="text" name="id" value={formData.id} disabled={true} />
            <br />
            <label>Name:</label>
            <br />
            <input type="text" name="name" value={formData.name} onChange={handleChange} />
            <br />
            <label>Calories Per Hour:</label>
            <br />
            <input type="number" name="calories_per_hour" value={formData.calories_per_hour} onChange={handleChange} />
            <br />
            <label>Weight:</label>
            <br />
            <input type="number" name="weight" value={formData.weight} onChange={handleChange} />
            <br />
            <label>Duration (minutes):</label>
            <br />
            <input type="number" name="duration_minutes" value={formData.duration_minutes} onChange={handleChange} />
            <br />
            <label>Total Calories:</label>
            <br />
            <input type="number" name="total_calories" value={formData.total_calories} onChange={handleChange} />
            <br />
            <button onClick={handleUpdateClick}>Update</button>
            <button onClick={clearData}>Cancel</button>
        </div>
    );
}

export default ActivityUpdateForm;