import { useState } from "react";
import "../index.css";
import AdminTableComponent from "../Components/Tables/AdminTableComponent";
import MealDataUpdateForm from "../Components/Forms/MealUpdateForm";
import ActivityUpdateForm from "../Components/Forms/ActivityUpdateForm";
import CocktailUpdateForm from "../Components/Forms/CocktailUpdateForm";
import UserUpdateForm from "../Components/Forms/UserUpdateForm";

function Admin() {
  const [mealData, setMealData] = useState([]);
  const [sportData, setSportData] = useState([]);
  const [drinksData, setDrinksData] = useState([]);
  const [usersData, setUsersData] = useState([]);
  const [mealPlanData, setMealPlanData] = useState([]);

  const [mealToUpdate, setMealToUpdate] = useState({});
  const [sportToUpdate, setSportToUpdate] = useState({});
  const [drinkToUpdate, setDrinkToUpdate] = useState({});
  const [userToUpdate, setUserToUpdate] = useState({});

  const [mealUpdateForm, setMealUpdateForm] = useState(false);
  const [sportUpdateForm, setSportUpdateForm] = useState(false);
  const [drinkUpdateForm, setDrinkUpdateForm] = useState(false);
  const [userUpdateForm, setUserUpdateForm] = useState(false);

  const [showMealDataTable, setShowMealDataTable] = useState(false);
  const [showSportDataTable, setShowSportDataTable] = useState(false);
  const [showDrinksDataTable, setShowDrinksDataTable] = useState(false);
  const [showUsersDataTable, setShowUsersDataTable] = useState(false);
  const [showMealPlanDataTable, setShowMealPlanDataTable] = useState(false);

  const [loadingScreen, setLoadingScreen] = useState(false);

  // Meal CRUD operations
  async function fetchMealData() {
    try {
      setLoadingScreen(true);
      const response = await fetch("/api/meal/getAll", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      setMealData(data);
      setLoadingScreen(false);
    } catch (error) {
      console.error(error);
    }
  }

  async function handleMealDelete(id) {
    try {
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
    } catch (error) {
      console.error(error);
    }
  }

  async function handleMealUpdate(id, data) {
    try {
      const response = await fetch(`api/meal/update/${id}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
      if (response.ok) {
        setMealData(mealData.map((row) => (row.id === data.id ? { ...row, ...data } : row)));
      }
    } catch (error) {
      console.error(error);
    }
  }

  function handleMealDataToUpdate(data) {
    setMealToUpdate(data);
    setMealUpdateForm(true);
    setShowMealDataTable(false);
  }

  function clearMealData() {
    setMealToUpdate({});
    setMealUpdateForm(false);
    setShowMealDataTable(true);
  }

  // Sport CRUD operations
  async function fetchSportData() {
    try {
      setLoadingScreen(true);
      const response = await fetch("/api/activities/getAll", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      setSportData(data);
      setLoadingScreen(false);
    } catch (error) {
      console.error(error);
    }
  }

  async function handleSportDelete(id) {
    try {
      const response = await fetch(`api/activities/delete/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      if (data) {
        const newData = sportData.filter((sport) => sport.id !== id);
        setSportData(newData);
      }
    } catch (error) {
      console.error(error);
    }
  }

  async function handleSportUpdate(id, data) {
    try {
      const response = await fetch(`api/activities/update/${id}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
      if (response.ok) {
        setSportData(sportData.map((row) => (row.id === data.id ? { ...row, ...data } : row)));
      }
    } catch (error) {
      console.error(error);
    }
  }

  function handleSportDataToUpdate(data) {
    setSportToUpdate(data);
    setSportUpdateForm(true);
    setShowSportDataTable(false);
  }

  function clearSportData() {
    setSportToUpdate({});
    setSportUpdateForm(false);
    setShowSportDataTable(true);
  }

  // Drinks CRUD operations
  async function fetchDrinksData() {
    try {
      setLoadingScreen(true);
      const response = await fetch("/api/cocktail/getAll", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      setDrinksData(data);
      setLoadingScreen(false);
    } catch (error) {
      console.error(error);
    }
  }

  async function handleDrinksDelete(id) {
    try {
      const response = await fetch(`api/cocktail/delete/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      if (data) {
        const newData = drinksData.filter((drink) => drink.id !== id);
        setDrinksData(newData);
      }
    } catch (error) {
      console.error(error);
    }
  }

  async function handleDrinksUpdate(id, data) {
    try {
      const response = await fetch(`api/cocktail/update/${id}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
      if (response.ok) {
        setDrinksData(
          drinksData.map((row) => (row.id === data.id ? { ...row, ...data } : row))
        );
      }
    } catch (error) {
      console.error(error);
    }
  }

  function handleDrinksDataToUpdate(data) {
    setDrinkToUpdate(data);
    setDrinkUpdateForm(true);
    setShowDrinksDataTable(false);
  }

  function clearDrinksData() {
    setDrinkToUpdate({});
    setDrinkUpdateForm(false);
    setShowDrinksDataTable(true);
  }

  // Users CRUD operations
  async function fetchUsersData() {
    try {
      setLoadingScreen(true);
      const response = await fetch("/api/User/getAll", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      setUsersData(data);
      setLoadingScreen(false);
    } catch (error) {
      console.error(error);
    }
  }

  async function handleUsersDelete(id) {
    try {
      const response = await fetch(`api/User/delete/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      if (data) {
        const newData = usersData.filter((user) => user.id !== id);
        setUsersData(newData);
      }
    } catch (error) {
      console.error(error);
    }
  }

  async function handleUsersUpdate(id, data) {
    try {
      const response = await fetch(`api/User/update/${id}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
      if (response.ok) {
        setUsersData(usersData.map((row) => (row.id === id ? { ...row, ...data } : row)));
      }
    } catch (error) {
      console.error(error);
    }
  }

  function handleUsersDataToUpdate(data) {
    setUserToUpdate(data);
    setUserUpdateForm(true);
    setShowUsersDataTable(false);
  }

  function clearUsersData() {
    setUserToUpdate({});
    setUserUpdateForm(false);
    setShowUsersDataTable(true);
  }

  // MealPlan only getall method
  async function fetchMealPlanData() {
    try {
      setLoadingScreen(true);
      const response = await fetch("/api/mealplan/getAll", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = await response.json();
      data.forEach((element) => {
        element.date = element.date.split("T")[0];
      });
      setMealPlanData(data);
      setLoadingScreen(false);
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <div>
      <h1>Admin Page</h1>
      <br />
      <div>
        <button
          onClick={() => {
            setShowMealDataTable(true);
            setShowSportDataTable(false);
            setShowDrinksDataTable(false);
            setShowUsersDataTable(false);
            setShowMealPlanDataTable(false);
            setMealUpdateForm(false);
            setDrinkUpdateForm(false);
            setSportUpdateForm(false);
            setUserUpdateForm(false);
            fetchMealData();
          }}
          disabled={showMealDataTable}
        >
          Show Meal Data
        </button>
        <button
          onClick={() => {
            setShowSportDataTable(true);
            setShowMealDataTable(false);
            setShowDrinksDataTable(false);
            setShowUsersDataTable(false);
            setShowMealPlanDataTable(false);
            setMealUpdateForm(false);
            setDrinkUpdateForm(false);
            setSportUpdateForm(false);
            setUserUpdateForm(false);
            fetchSportData();
          }}
          disabled={showSportDataTable}
        >
          Show Sport Data
        </button>
        <button
          onClick={() => {
            setShowDrinksDataTable(true);
            setShowMealDataTable(false);
            setShowSportDataTable(false);
            setShowUsersDataTable(false);
            setShowMealPlanDataTable(false);
            setMealUpdateForm(false);
            setDrinkUpdateForm(false);
            setSportUpdateForm(false);
            setUserUpdateForm(false);
            fetchDrinksData();
          }}
          disabled={showDrinksDataTable}
        >
          Show Drinks Data
        </button>
        <button
          onClick={() => {
            setShowUsersDataTable(true);
            setShowMealDataTable(false);
            setShowSportDataTable(false);
            setShowDrinksDataTable(false);
            setShowMealPlanDataTable(false);
            setMealUpdateForm(false);
            setDrinkUpdateForm(false);
            setSportUpdateForm(false);
            setUserUpdateForm(false);
            fetchUsersData();
          }}
          disabled={showUsersDataTable}
        >
          Show Users Data
        </button>
        <button
          onClick={() => {
            setShowMealPlanDataTable(true);
            setShowUsersDataTable(false);
            setShowMealDataTable(false);
            setShowSportDataTable(false);
            setShowDrinksDataTable(false);
            setMealUpdateForm(false);
            setDrinkUpdateForm(false);
            setSportUpdateForm(false);
            setUserUpdateForm(false);
            fetchMealPlanData();
          }}
          disabled={showMealPlanDataTable}
        >
          Show Meal Plan Data
        </button>
      </div>
      <div>
        {showMealDataTable &&
        !showDrinksDataTable &&
        !showSportDataTable &&
        !showUsersDataTable &&
        !showMealPlanDataTable &&
        !mealUpdateForm &&
        !sportUpdateForm &&
        !drinkUpdateForm &&
        !userUpdateForm &&
        loadingScreen ? (
          <h2>Loading Meal Data...</h2>
        ) : !showMealDataTable &&
          showDrinksDataTable &&
          !showSportDataTable &&
          !showUsersDataTable &&
          !showMealPlanDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm &&
          loadingScreen ? (
          <h2>Loading Drinks Data...</h2>
        ) : !showMealDataTable &&
          !showDrinksDataTable &&
          showSportDataTable &&
          !showUsersDataTable &&
          !showMealPlanDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm &&
          loadingScreen ? (
          <h2>Loading Sport Data...</h2>
        ) : !showMealDataTable &&
          !showDrinksDataTable &&
          !showSportDataTable &&
          showUsersDataTable &&
          !showMealPlanDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm &&
          loadingScreen ? (
          <h2>Loading Users Data...</h2>
        ) : showMealPlanDataTable &&
          !showUsersDataTable &&
          !showMealDataTable &&
          !showDrinksDataTable &&
          !showSportDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm &&
          loadingScreen ? (
          <h2>Loading Meal Plan Data...</h2>
        ) : showUsersDataTable &&
          !showMealDataTable &&
          !showDrinksDataTable &&
          !showSportDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm ? (
          <div className="table">
            <AdminTableComponent
              dataArray={usersData}
              onDelete={handleUsersDelete}
              onEdit={handleUsersDataToUpdate}
            />
          </div>
        ) : showMealDataTable &&
          !showDrinksDataTable &&
          !showSportDataTable &&
          !showUsersDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm ? (
          <div className="table">
            <AdminTableComponent
              dataArray={mealData}
              onDelete={handleMealDelete}
              onEdit={handleMealDataToUpdate}
            />
          </div>
        ) : !showMealDataTable &&
          showDrinksDataTable &&
          !showSportDataTable &&
          !showUsersDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm ? (
          <div className="table">
            <AdminTableComponent
              dataArray={drinksData}
              onDelete={handleDrinksDelete}
              onEdit={handleDrinksDataToUpdate}
            />
          </div>
        ) : !showMealDataTable &&
          !showDrinksDataTable &&
          showSportDataTable &&
          !showUsersDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm ? (
          <div className="table">
            <AdminTableComponent
              dataArray={sportData}
              onDelete={handleSportDelete}
              onEdit={handleSportDataToUpdate}
            />
          </div>
        ) : showMealPlanDataTable &&
          !showUsersDataTable &&
          !showMealDataTable &&
          !showDrinksDataTable &&
          !showSportDataTable &&
          !mealUpdateForm &&
          !sportUpdateForm &&
          !drinkUpdateForm &&
          !userUpdateForm ? (
          <div className="table">
            <AdminTableComponent dataArray={mealPlanData} />
          </div>
        ) : mealUpdateForm && !drinkUpdateForm && !sportUpdateForm && !userUpdateForm ? (
          <MealDataUpdateForm
            data={mealToUpdate}
            onUpdate={handleMealUpdate}
            clearData={clearMealData}
          />
        ) : !mealUpdateForm && drinkUpdateForm && !sportUpdateForm && !userUpdateForm ? (
          <CocktailUpdateForm
            data={drinkToUpdate}
            onUpdate={handleDrinksUpdate}
            clearData={clearDrinksData}
          />
        ) : !mealUpdateForm && !drinkUpdateForm && sportUpdateForm && !userUpdateForm ? (
          <ActivityUpdateForm
            data={sportToUpdate}
            onUpdate={handleSportUpdate}
            clearData={clearSportData}
          />
        ) : !mealUpdateForm && !drinkUpdateForm && !sportUpdateForm && userUpdateForm ? (
          <UserUpdateForm
            data={userToUpdate}
            onUpdate={handleUsersUpdate}
            clearData={clearUsersData}
          />
        ) : (
          <h2>Waiting for instructions...</h2>
        )}
      </div>
    </div>
  );
}

export default Admin;