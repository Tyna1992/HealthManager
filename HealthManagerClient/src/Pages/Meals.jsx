import { useState } from "react";
import "../index.css";
import TableComponent from "../Components/TableComponent";

function Meals(props) {
  const [meal, setMeal] = useState("");
  const [amount, setAmount] = useState("");
  const [nutritionalValue, setNutritionalValue] = useState([]);
  const [hideResult, setHideResult] = useState(true);

  const fetchNutritionalValue = async () => {
    if (amount === "" || meal === "") {
      alert("Please fill out both fields.");
      return;
    }
    try {
      const response = await fetch(`/api/meal/${amount}/${meal}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (!response.ok) {
        alert("Cannot find the nutritional value for the meal. Please try again.");
        throw new Error("Fetch failed!");
      }
      const data = await response.json();
      if (data.length === 0) {
        alert("Cannot find the nutritional value for the meal. Please try again.");
        throw new Error("Cannot find the nutritional value for the meal. Please try again.");
      }
      setNutritionalValue(data);
      console.log(data);
      // console.log(Object.keys(data[0]));
      setHideResult(false);
    } catch (error) {
      console.error(error);
    }
  };

  const handleClear = () => {
    setMeal("");
    setAmount("");
  };

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
      <input
        required
        type="text"
        name="meal"
        value={meal}
        placeholder="Name of the meal"
        onChange={handleMealChange}
      ></input>
      <br></br>
      <label>Serving size:</label>
      <br></br>
      <input
        required
        type="text"
        name="amount"
        value={amount}
        placeholder="In gramms"
        onChange={handleAmountChange}
      ></input>
      <br></br>
      <button onClick={() => fetchNutritionalValue()}>Show nutritional value</button>
      <button
        onClick={() => {
          setHideResult(true);
          handleClear();
        }}
      >
        Clear
      </button>
      <br />
      {
        nutritionalValue.length !== 0 ? 
      (<div className="mealTable" hidden={hideResult}>
        <TableComponent dataArray={nutritionalValue} />
      </div>) : "" 
      }
    </div>
  );
}

export default Meals;
