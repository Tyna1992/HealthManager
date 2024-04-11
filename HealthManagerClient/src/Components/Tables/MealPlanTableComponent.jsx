import React, { useState, useEffect } from "react";
import "../../index.css";
import { capitalizeWords } from "../../../utilities/utils";


function MealPlanTableComponent({dataArray}){
    const [dataKeys, setDataKeys] = useState([]);

    useEffect(() => {
        console.log(dataArray);
        let columns = Object.keys(dataArray[0]);
        let filteredColumns = columns.filter((column) => column !== "id" && column !== "mealId" && column !== "userName");
        setDataKeys(filteredColumns);
    }, []);


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
                    </tr>
                    {dataArray.map((data) => (
                        <tr key={data.id}>
                            <td>{capitalizeWords(data.meal.name)}</td>
                            <td>{data.meal.serving_size_g} g</td>
                            <td>{data.meal.calories} cal</td>
                            <td>{data.date.split("T")[0]}</td>
                            <td>{capitalizeWords(data.mealTime)}</td>
                            <td>{data.dayOfTheWeek}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default MealPlanTableComponent;