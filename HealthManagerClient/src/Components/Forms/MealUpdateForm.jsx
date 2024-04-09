import React, { useState } from 'react';
import "../../index.css";

function MealDataUpdateForm({data, onUpdate, clearData}){
    const [formData, setFormData] = useState({
        id: data.id,
        name: data.name,
        serving_size_g: data.serving_size_g,
        calories: data.calories,
        fat_total_g: data.fat_total_g,
        fat_saturated_g: data.fat_saturated_g,
        protein_g: data.protein_g,
        sodium_mg: data.sodium_mg,
        potassium_mg: data.potassium_mg,
        cholesterol_mg: data.cholesterol_mg,
        carbohydrates_total_g: data.carbohydrates_total_g,
        fiber_g: data.fiber_g,
        sugar_g: data.sugar_g
    });

    const handleChange = (e) => {
        setFormData({...formData, [e.target.name]: e.target.value});
    }

    const handleUpdateClick = () => {
        onUpdate(data.id, formData);
        clearData();
    }

    return (
        <div className="form-container">
            <h1>Update Meal Data</h1>
            <br />
            <label>Id:</label>
            <br />
            <input type="number" name="id" value={formData.id} disabled={true} />
            <br />
            <label>Name:</label>
            <br />
            <input type="text" name="name" value={formData.name} onChange={handleChange} />
            <br />
            <label>Serving Size (g):</label>
            <br />
            <input type="number" name="serving_size_g" value={formData.serving_size_g} onChange={handleChange} />
            <br />
            <label>Calories:</label>
            <br />
            <input type="number" name="calories" value={formData.calories} onChange={handleChange} />
            <br />
            <label>Total Fat (g):</label>
            <br />
            <input type="number" name="fat_total_g" value={formData.fat_total_g} onChange={handleChange} />
            <br />
            <label>Saturated Fat (g):</label>
            <br />
            <input type="number" name="fat_saturated_g" value={formData.fat_saturated_g} onChange={handleChange} />
            <br />
            <label>Protein (g):</label>
            <br />
            <input type="number" name="protein_g" value={formData.protein_g} onChange={handleChange} />
            <br />
            <label>Sodium (mg):</label>
            <br />
            <input type="number" name="sodium_mg" value={formData.sodium_mg} onChange={handleChange} />
            <br />
            <label>Potassium (mg):</label>
            <br />
            <input type="number" name="potassium_mg" value={formData.potassium_mg} onChange={handleChange} />
            <br />
            <label>Cholesterol (mg):</label>
            <br />
            <input type="number" name="cholesterol_mg" value={formData.cholesterol_mg} onChange={handleChange} />
            <br />
            <label>Carbohydrates (g):</label>
            <br />
            <input type="number" name="carbohydrates_total_g" value={formData.carbohydrates_total_g} onChange={handleChange} />
            <br />
            <label>Fiber (g):</label>
            <br />
            <input type="number" name="fiber_g" value={formData.fiber_g} onChange={handleChange} />
            <br />
            <label>Sugar (g):</label>
            <br />
            <input type="number" name="sugar_g" value={formData.sugar_g} onChange={handleChange} />
            <br />
            <button onClick={handleUpdateClick}>Update</button>
            <button onClick={clearData}>Cancel</button>
        </div>
    );
}

export default MealDataUpdateForm;