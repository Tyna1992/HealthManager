import React from "react";
import "../../index.css";
import { capitalizeWords } from "../../../utilities/utils";

function MealPlanTableComponent({ dataArray, onDelete, onEdit }) {
  return (
    <div className="table">
      <table>
        <tbody>
          <tr>
            <th>Meal name</th>
            <th>Serving size</th>
            <th>Calories</th>
            <th>Date</th>
            <th>Meal time</th>
            <th>Day</th>
            <th>Delete</th>
            <th>Edit</th>
          </tr>
          {dataArray.map((data) => (
            <tr key={data.id}>
              <td>{capitalizeWords(data.meal.name)}</td>
              <td>{data.meal.serving_size_g} g</td>
              <td>{data.meal.calories} cal</td>
              <td>{data.date.split("T")[0]}</td>
              <td>{capitalizeWords(data.mealTime)}</td>
              <td>{data.dayOfTheWeek}</td>
              <td>
                <button onClick={() => onDelete(data.id)}>Delete</button>
              </td>
              <td>
                <button onClick={() => onEdit(data)} disabled>
                  Edit
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default MealPlanTableComponent;