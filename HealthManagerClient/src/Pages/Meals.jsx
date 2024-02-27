import { useState } from "react";
import "../index.css";


function Meals(props) {

    const [meal, setMeal] = useState("");
    const [amount, setAmount] = useState("");
    const [nutritionalValue, setNutritionalValue] = useState({});
    const [hideResult, setHideResult] = useState(true);


    const fetchNutritionalValue = async () => {
        const API_KEY = "pe8bDjzt2qs1AeJNbzukUw==sXVqA6FU8t4IBEFG";
        const API_URL = `https://api.api-ninjas.com/v1/nutrition/?query=${amount} ${meal}`;
        try {
            const response = await fetch(API_URL, {
                method: "GET",
                headers: {
                    "X-Api-Key": API_KEY,
                    "Content-Type": "application/json"
                }
            })
            if(!response.ok){
                throw new Error("Fetch failed!")
            }
            const data = await response.json();
            if(data.length === 0){
                alert("Cannot find the nutritional value for the meal. Please try again.")
                throw new Error("Cannot find the nutritional value for the meal. Please try again.") 
            }
            setNutritionalValue(data[0]);
            console.log(data);
            setHideResult(false);
        } catch (error) {
            console.error(error);
        }
    }

    const handleClear = () =>{
        setMeal("");
        setAmount("");
    }

    const handleMealChange = (event) => {
        setMeal(event.target.value);
    };

    const handleAmountChange = (event) => {
        setAmount(event.target.value);
    };
    return (
        <div>
            <label>Meal name:</label>
            <br></br>
            <input required={true} type="text" name="meal" value={meal} placeholder="Name of the meal" onChange={handleMealChange}></input>
            <br></br>
            <label>Serving size:</label>
            <br></br>
            <input required={true} type="text" name= "amount" value={amount} placeholder="In gramms" onChange={handleAmountChange}></input>
            <br></br>
            <button onClick={() => fetchNutritionalValue()}>Show nutritional value</button>
            <button onClick={()=> {setHideResult(true);handleClear()}}>Clear</button>

            <div hidden={hideResult} id="result">
                <h2>Nutritional value of {nutritionalValue.name}</h2>
                <p>Serving size: {nutritionalValue.serving_size_g} g</p>
                <p>Calories: {nutritionalValue.calories} cal</p>
                <p>Protein: {nutritionalValue.protein_g} g</p>
                <p>Fat: {nutritionalValue.fat_total_g} g</p>
                <p>Carbohydrates: {nutritionalValue.carbohydrates_total_g} g</p>
                <p>Sugar: {nutritionalValue.sugar_g} g</p>
                <p>Sodium: {nutritionalValue.sodium_mg} mg</p>
            </div>
        </div>
    )
}

export default Meals;