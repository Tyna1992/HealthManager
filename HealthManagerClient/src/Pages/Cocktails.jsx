import React, { useState } from "react";

function Cocktails(props) {
    const [cocktail, setCocktail] = useState("");
    const [cocktailDatas, setCocktailDatas ] = useState({});
    const [hideResult, setHideResult] = useState(true);
    const [ingredients, setIngredients] = useState([]);

    const fetchCocktailDatas = async () => {

        try {
            const response = await fetch(`/api/cocktail/${cocktail}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            });
            if(!response.ok){
                alert("Cannot find the cocktail. Please try again.");
                throw new Error("Fetch failed!");
            }
            const data = await response.json();
            if(data.length === 0) {
                alert("Cannot find the cocktail. Please try again.");
                throw new Error("Cannot find data about the cocktail. Please try again.");
            }
            setCocktailDatas(data);
            setIngredients(data.ingredients.split(","));
            setHideResult(false);
        } catch (error) {
            console.error(error);
        }
    }
    
    const handleClear = () => {
        setCocktail("");
    }

    const handleCocktailChange = (event) => {
        setCocktail(event.target.value)
    }

    return (
        <div>
            <label>Cocktail name:</label>
            <br></br>
            <input required type="text" name="coctail" value={cocktail} onChange={handleCocktailChange}/>
            <br></br>
            <button onClick={fetchCocktailDatas}>Search cocktail</button>
            <button onClick={() => {setHideResult(true); handleClear()}}>Clear</button>

            {<div hidden={hideResult}>
                <h2>{cocktailDatas.name}</h2>
                <h3>Ingredients:</h3>
                {ingredients.map(ingredient => {
                    return <p>{ingredient}</p>
                })}
                <p>Instructions:</p>
                <p>{cocktailDatas.instructions}</p>
            </div>}
        </div>
    )
}

export default Cocktails;