import React, { useState } from 'react';
import "../../index.css";

function CocktailUpdateForm({ data, onEdit, clearData }) {
    const [formData, setFormData] = useState({
        id: data.id,
        name: data.name,
        ingredients: data.ingredients,
        instructions: data.instructions
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
            <h2>Update Cocktail</h2>
            <br />
            <label>Id:</label>
            <br />
            <input type="text" name="id" value={formData.id} disabled={true} />
            <br />
            <label>Name:</label>
            <br />
            <input type="text" name="name" value={formData.name} onChange={handleChange} />
            <br />
            <label>Ingredients:</label>
            <br />
            <textarea name="ingredients" value={formData.ingredients} onChange={handleChange} />
            <br />
            <label>Instructions:</label>
            <br />
            <textarea name="instructions" value={formData.instructions} onChange={handleChange} />
            <br />
            <button onClick={handleUpdateClick}>Update</button>
            <button onClick={clearData}>Cancel</button>
        </div>
    );
}

export default CocktailUpdateForm;