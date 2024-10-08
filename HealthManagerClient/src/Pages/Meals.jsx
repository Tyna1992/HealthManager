﻿import { useState } from "react";
import "../index.css";
import TableComponent from "../Components/Tables/TableComponent";
import { toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function Meals() {
  const [meal, setMeal] = useState("");
  const [amount, setAmount] = useState("");
  const [nutritionalValue, setNutritionalValue] = useState([]);

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
        toast.error("Cannot find the nutritional value for the meal. Please try again.",{
            position: "top-right"
            
        });
        throw new Error("Fetch failed!");
      }
      const data = await response.json();
      if (data.length === 0) {
        toast.error("Cannot find the nutritional value for the meal. Please try again.",{
          position: "top-right"

        });
        throw new Error("Cannot find the nutritional value for the meal. Please try again.");
      }
      setNutritionalValue(data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleClear = () => {
    setMeal("");
    setAmount("");
    setNutritionalValue([]);
  };

  const handleMealChange = (event) => {
    setMeal(event.target.value);
  };

  const handleAmountChange = (event) => {
    setAmount(event.target.value);
  };

  return (
    <div>
      <div className="container">
        <div className="input-fields">
          <label>Meal name:</label>
          <br />
          <input
            required
            type="text"
            name="meal"
            value={meal}
            placeholder="Name of the meal"
            onChange={handleMealChange}
          ></input>
        </div>
        <br />
        <div className="input-fields">
          <label>Serving size:</label>
          <br />
          <input
            required
            type="text"
            name="amount"
            value={amount}
            placeholder="In gramms"
            onChange={handleAmountChange}
          ></input>
        </div>
      </div>
      <button onClick={() => fetchNutritionalValue()}>Show nutritional value</button>
      <button onClick={() => handleClear()}>Clear</button>
      <br />
      {nutritionalValue.length !== 0 ? (
        <div className="mealTable">
          <TableComponent dataArray={nutritionalValue} />
        </div>
      ) : null}
    </div>
  );
}

export default Meals;