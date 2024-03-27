import React, { useState } from 'react';
import { capitalizeWords } from '../../utilities/utils';
import TableComponent from "../Components/TableComponent.jsx";

function Activities(props) {

    const [activity, setActivity] = useState([]);
    const [activityName, setActivityName] = useState("");
    const [duration, setDuration] = useState("");
    const [weight, setWeight] = useState("");
    const [hideResult, setHideResult] = useState(true);

    const fetchActivity = async () => {
        if (activityName === ""){
            alert("Please fill out the activity name.")
            return;
        }
        try {
            const response = await fetch(`/api/activities/${activityName}/${weight}/${duration}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            })
            console.log(response);
            if(!response.ok){
                alert("Cannot find the activity. Please try again.")
                throw new Error("Fetch failed!")
            }
            const data = await response.json();
            console.log(data);
            if(data.length === 0){
                alert("Cannot find the activity. Please try again.")
                throw new Error("Cannot find the activity. Please try again.") 
            }
            setActivity(data);
            setHideResult(false);
            console.log(activity)
        }
        catch (error) {
            console.error(error);
        }
    }

    const handleClear = () =>{
        setActivityName("");
        setDuration("");
        setWeight("");
    }

    const handleActivityChange = (event) => {
        setActivityName(event.target.value);
    };

    const handleDurationChange = (event) => {
        setDuration(event.target.value);
    };

    const handleWeightChange = (event) => {
        setWeight(event.target.value);
    };

return (
    <div>
        <label>Activity name:</label>
        <br></br>
        <input required type="text" name="activity" value={activityName} placeholder="Name of the activity" onChange={handleActivityChange}></input>
        <br></br>
        <label>Duration (in minutes):</label>
        <br></br>
        <input type="text" name="duration" value={duration} placeholder="Duration of the activity" onChange={handleDurationChange}></input>
        <br></br>
        <label>Weight (in pounds):</label>
        <br></br>
        <input type="text" name="weight" value={weight} placeholder="Weight of the person" onChange={handleWeightChange}></input>
        <br></br>
        <button onClick={fetchActivity}>Search activity</button>
        <button onClick={() => {setHideResult(true); handleClear()}}>Clear</button>

        { activity.length !== 0 ? (
            <div className="activityTable">
                <TableComponent dataArray={activity}></TableComponent>
            </div>
        ) :(
            ""
        )}
      
    </div>
  );
};

export default Activities;
