import { useState } from "react";
import "../index.css";


function Meals(props) {

    const [meal, setMeal] = useState("");
    const [amount, setAmount] = useState("");
    const [nutritionalValue, setNutritionalValue] = useState({});
    const [hideResult, setHideResult] = useState(true);


    const fetchNutritionalValue = async () => {
        const API_KEY = "pe8bDjzt2qs1AeJNbzukUw==sXVqA6FU8t4IBEFG";
        const API_URL = `https://api.api-ninjas.com/v1/nutrition/?query=${amount}g ${meal}`;
        try {
            const response = await fetch(API_URL, {
                method: "GET",
                headers: {
                    "X-Api-Key": API_KEY,
                    "Content-Type": "application/json"
                }
            })
            if(!response.ok){
                throw new Error("Failed to fetch nutritional value")
            }
            const data = await response.json();
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
            <input type="text" name="meal" value={meal} onChange={handleMealChange}></input>
            <br></br>
            <label>Meal amount in gramms:</label>
            <br></br>
            <input type="text" name= "amount" value={amount} onChange={handleAmountChange}></input>
            <br></br>
            <button onClick={() => fetchNutritionalValue()}>Show nutritional value</button>
            <button onClick={()=> {setHideResult(true);handleClear()}}>Clear</button>

            <div hidden={hideResult} id="result">
                <h2>Nutritional value</h2>
                <p>Calories: {nutritionalValue.calories}</p>
                <p>Protein: {nutritionalValue.protein_g}</p>
                <p>Fat: {nutritionalValue.fat_total_g}</p>
                <p>Carbohydrates: {nutritionalValue.carbohydrates_total_g}</p>
            </div>
        </div>
    )
}

export default Meals;