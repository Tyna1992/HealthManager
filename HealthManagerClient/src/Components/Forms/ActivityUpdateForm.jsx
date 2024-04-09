import React, { useState } from 'react';
import "../../index.css";

function ActivityUpdateForm({ data, onEdit, clearData }) {
    const [formData, setFormData] = useState({
        id: data.id,
        name: data.name,
        caloriesPerHour: data.calories_per_hour,
        weight: data.weight,
        durationMinutes: data.duration_minutes,
        totalCalories: data.total_calories
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    }

    const handleUpdateClick = () => {
        onEdit(formData);
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
            <input type="number" name="caloriesPerHour" value={formData.caloriesPerHour} onChange={handleChange} />
            <br />
            <label>Weight:</label>
            <br />
            <input type="number" name="weight" value={formData.weight} onChange={handleChange} />
            <br />
            <label>Duration (minutes):</label>
            <br />
            <input type="number" name="durationMinutes" value={formData.durationMinutes} onChange={handleChange} />
            <br />
            <label>Total Calories:</label>
            <br />
            <input type="number" name="totalCalories" value={formData.totalCalories} onChange={handleChange} />
            <br />
            <button onClick={handleUpdateClick}>Update</button>
            <button onClick={clearData}>Cancel</button>
        </div>
    );
}

export default ActivityUpdateForm;