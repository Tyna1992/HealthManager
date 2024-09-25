import React, { useState } from "react";
import TableComponent from "../Components/Tables/TableComponent";
import {toast} from "react-toastify";

function Cocktails() {
  const [cocktail, setCocktail] = useState("");
  const [cocktailDatas, setCocktailDatas] = useState([]);

  const fetchCocktailDatas = async () => {
    try {
      const response = await fetch(`/api/cocktail/${cocktail}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (!response.ok) {
        toast.error("Cannot find the requested drink. Please try again!", {
          position: "top-right"
        });
        throw new Error("Fetch failed!");
      }
      const data = await response.json();
      if (data.length === 0) {
        toast.error("Cannot find the requested drink. Please try again!", {
          position: "top-right"
        });
        throw new Error("Cannot find data about the cocktail. Please try again.");
      }
      setCocktailDatas(data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleClear = () => {
    setCocktail("");
  };

  const handleCocktailChange = (event) => {
    setCocktail(event.target.value);
  };

  return (
    <div>
      <label>Cocktail name:</label>
      <br></br>
      <input
        required
        type="text"
        name="cocktail"
        value={cocktail}
        onChange={handleCocktailChange}
      />
      <br></br>
      <button onClick={fetchCocktailDatas}>Search cocktail</button>
      <button onClick={() => handleClear()}>Clear</button>
      {cocktailDatas.length !== 0 ? (
        <div className="cocktailTable">
          <TableComponent dataArray={cocktailDatas}></TableComponent>
        </div>
      ) : null}
    </div>
  );
}

export default Cocktails;