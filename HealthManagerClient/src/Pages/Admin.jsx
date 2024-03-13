import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../index.css";
import AdminTableComponent from "../Components/AdminTableComponent";

function Admin() {
  const [mealData, setMealData] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const response = await fetch("api/meal/getAll", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      setMealData(data);
    }
    fetchData();
  }, [mealData]);

  async function handleDelete(id) {
    const response = await fetch(`api/meal/delete/${id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const data = await response.json();
    if (data) {
      const newData = mealData.filter((meal) => meal.id !== id);
      setMealData(newData);
    }
  }

  async function handleUpdate(id) {
    const response = await fetch(`api/meal/update/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const data = await response.json();
    console.log(data)
    if(data){
        setMealData(data);
    }
  }

  return (
    <div>
      <h1>Welcome, my Master! UwU nani???</h1>
      <div className="table">
        {mealData.length === 0 ? <h1>No data to display</h1> : (

        <AdminTableComponent
          dataArray={mealData}
          onDelete={handleDelete}
          onEdit={handleUpdate}
        />
        )}
      </div>
    </div>
  );
}

export default Admin;
