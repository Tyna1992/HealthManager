import React, { useState } from 'react';
import "../../index.css";

function MealDataUpdateForm({data, onEdit, clearData}){
    const [formData, setFormData] = useState({
        id: data.id,
        name: data.name,
        servingSize: data.serving_size_g,
        calories: data.calories,
        totalFat: data.fat_total_g,
        saturatedFat: data.fat_saturated_g,
        protein: data.protein_g,
        sodium: data.sodium_mg,
        potassium: data.potassium_mg,
        cholesterol: data.cholesterol_mg,
        carbohydrates: data.carbohydrates_total_g,
        fiber: data.fiber_g,
        sugar: data.sugar_g
    });

    const handleChange = (e) => {
        setFormData({...formData, [e.target.name]: e.target.value});
    }

    const handleUpdateClick = () => {
        onEdit(formData);
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
            <input type="text" name="servingSize" value={formData.servingSize} onChange={handleChange} />
            <br />
            <label>Calories:</label>
            <br />
            <input type="text" name="calories" value={formData.calories} onChange={handleChange} />
            <br />
            <label>Total Fat (g):</label>
            <br />
            <input type="text" name="totalFat" value={formData.totalFat} onChange={handleChange} />
            <br />
            <label>Saturated Fat (g):</label>
            <br />
            <input type="text" name="saturatedFat" value={formData.saturatedFat} onChange={handleChange} />
            <br />
            <label>Protein (g):</label>
            <br />
            <input type="text" name="protein" value={formData.protein} onChange={handleChange} />
            <br />
            <label>Sodium (mg):</label>
            <br />
            <input type="text" name="sodium" value={formData.sodium} onChange={handleChange} />
            <br />
            <label>Potassium (mg):</label>
            <br />
            <input type="text" name="potassium" value={formData.potassium} onChange={handleChange} />
            <br />
            <label>Cholesterol (mg):</label>
            <br />
            <input type="text" name="cholesterol" value={formData.cholesterol} onChange={handleChange} />
            <br />
            <label>Carbohydrates (g):</label>
            <br />
            <input type="text" name="carbohydrates" value={formData.carbohydrates} onChange={handleChange} />
            <br />
            <label>Fiber (g):</label>
            <br />
            <input type="text" name="fiber" value={formData.fiber} onChange={handleChange} />
            <br />
            <label>Sugar (g):</label>
            <br />
            <input type="text" name="sugar" value={formData.sugar} onChange={handleChange} />
            <br />
            <button onClick={handleUpdateClick}>Update</button>
            <button onClick={clearData}>Cancel</button>
        </div>
    );
}

export default MealDataUpdateForm;