import { useState, useEffect } from "react";
import "../index.css";
import TableComponent from "../Components/Tables/TableComponent";
import {toast} from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function Mealplan() {
  const today = new Date().toISOString().slice(0, 10);
  const [userData, setUserData] = useState({});
  const [nutritionalValue, setNutritionalValue] = useState([]);
  const [amount, setAmount] = useState("");
  const [meal, setMeal] = useState("");
  const [date, setDate] = useState(today);
  const [mealTime, setMealTime] = useState("");
  const [nutritionalValueShow, setNutritionalValueShow] = useState(false);

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await fetch("/api/Auth/WhoAmI", {
          method: "GET",
          credentials: "include",
          headers: {
            "Content-Type": "application/json",
          },
        });
        const data = await response.json();
        if (data) {
          setUserData(data);
        }
      } catch (error) {
        console.error(error);
      }
    }
    fetchData();
  }, []);

  const fetchNutritionalValue = async () => {
    if (amount === "" || meal === "") {
      toast.error("Please fill out the meal and serving size fields!",{
        position: "top-right"

      });
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
      setNutritionalValueShow(true);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchMealPlan = async () => {
    try {
      const response = await fetch("/api/MealPlan/create", {
        method: "POST",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          UserName: userData.userName,
          ServingSize: amount,
          Name: meal,
          Date: date,
          MealTime: mealTime,
        }),
      });
      if (!response.ok) {
        toast.error("Cannot add to the plan. Please try again.",{
          position: "top-right"

        });
        throw new Error("Fetch failed!");
      }
      toast.success("Meal added to the plan!",{
        position: "top-right"

      });
      window.location.reload();
    } catch (error) {
      console.error(error);
    }
  };

  const handleClear = () => {
    setMeal("");
    setAmount("");
    setNutritionalValue([]);
    setNutritionalValueShow(false);
  };

  const handleMealChange = (event) => {
    setMeal(event.target.value);
  };

  const handleAmountChange = (event) => {
    setAmount(event.target.value);
  };

  const handleDateChange = (event) => {
    setDate(event.target.value);
  };

  const handleMealTimeChange = (event) => {
    setMealTime(event.target.value);
  };

  return (
    <div>
      <h1>Welcome {userData.userName}!</h1>
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
          <br />
          <div className="input-fields">
            <label>Date:</label>
            <br />
            <input
              required
              type="date"
              value={date}
              name="date"
              onChange={handleDateChange}
              min={today}
            ></input>
          </div>
          <br />
          <div className="input-fields">
            <label>Meal time:</label>
            <br />
            <select value={mealTime} name="mealTime" onChange={handleMealTimeChange}>
              <option value="" disabled defaultValue={""}>
                Select meal time
              </option>
              <option value="breakfast">Breakfast</option>
              <option value="elevenses">Elevenses</option>
              <option value="lunch">Lunch</option>
              <option value="snack">Afternoon snack</option>
              <option value="dinner">Dinner</option>
            </select>
          </div>
          <br />
        </div>
        <button onClick={() => fetchNutritionalValue()}>Show nutritional value</button>
        <button onClick={() => handleClear()}>Clear</button>
        <button onClick={() => fetchMealPlan()} type="button" disabled={!nutritionalValueShow}>
          Add to the plan
        </button>
        <br />
        {nutritionalValue.length !== 0 ? (
          <div className="mealTable">
            <TableComponent dataArray={nutritionalValue} />
            <br />
          </div>
        ) : null}
      </div>
    </div>
  );
}

export default Mealplan;