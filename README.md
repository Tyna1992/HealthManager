# HealthManager Documentation

<p align="center">
		<em>Developed with the software and tools below.</em>
</p>
<p align="center">
	<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript" target="_blank">
  		<img src="https://img.shields.io/badge/JavaScript-F7DF1E.svg?style=flat&logo=JavaScript&logoColor=black" alt="JavaScript">
	</a>
	<a href="https://developer.mozilla.org/en-US/docs/Glossary/HTML5" target="_blank">
		<img src="https://img.shields.io/badge/HTML5-E34F26.svg?style=flat&logo=HTML5&logoColor=white" alt="HTML5">	
	</a>
	<a href="https://react.dev/reference/react" target="_blank">
		<img src="https://img.shields.io/badge/React-61DAFB.svg?style=flat&logo=React&logoColor=black" alt="React">
	</a>
 <a href="https://learn.microsoft.com/en-us/dotnet/csharp/" target="_blank">
	<img src="https://img.shields.io/badge/C%23-239120.svg?style=flat&logo=c-sharp&logoColor=white" alt="C#">
</a>
<a href="https://learn.microsoft.com/en-us/sql/sql-server/" target="_blank">
	<img src="https://img.shields.io/badge/MS_SQL_Server-CC2927.svg?style=flat&logo=microsoftsqlserver&logoColor=white" alt="MS SQL Server">
</a>
<a href="https://www.npmjs.com/" target="_blank">
  	<img src="https://img.shields.io/badge/NPM-CB3837.svg?style=flat&logo=npm&logoColor=white" alt="NPM">
</a>
<a href="https://www.nuget.org/" target="_blank">
  	<img src="https://img.shields.io/badge/NuGet-004880.svg?style=flat&logo=nuget&logoColor=white" alt="NuGet">
</a>
<a href="https://www.docker.com/" target="_blank">
  	<img src="https://img.shields.io/badge/Docker-2496ED.svg?style=flat&logo=docker&logoColor=white" alt="Docker">
</a>

</p>

![Health Manager Home page](./Assets/health_manager.png)

## Table of Contents
- [Introduction](#introduction)
- [Usage](#usage)
- [API](#api)

## Introduction
HealthManager is a simple, yet versatile application that allows users to track their health and fitness goals.

## Usage
To use the application, users can create an account and log in. Once logged in, users can search for meals, exercises, and cocktails. Users can add meals to their daily intake, create meal plans, and track their daily caloric intake. Users can also search for exercises based on their preferences and weight. Users can also search for cocktails and view their recipes. Admin can view all the data of the meals, exercises, and cocktails that has been added to the database. Admin can also delete or update these data. 

## API
The API is a RESTful API that allows users to interact with the database. The API has the following endpoints:

### Meals

- `/api/meal/{servingSize}/{name}`: Get a meal by its name and serving size.
- `/api/meal/getAll`: Get all meals in the database. Admin only.
- `/api/meal/delete/{id}`: Delete a meal by its id. Admin only.
- `/api/meal/update/{id}`: Update a meal by its id. Admin only.

### Activities

- `/api/activities/{activityName}/{weight}/{duration}`: Get an activity by its name, weight, and duration.
- `/api/activities/getAll`: Get all activities in the database. Admin only.
- `/api/activities/delete/{id}`: Delete an activity by its id. Admin only.
- `/api/activities/update/{id}`: Update an activity by its id. Admin only.

### Cocktails

- `/api/cocktail/{cocktailName}`: Get a cocktail by its name.
- `/api/cocktail/getAll`: Get all cocktails in the database. Admin only.
- `/api/cocktail/delete/{id}`: Delete a cocktail by its id. Admin only.
- `/api/cocktail/update/{id}`: Update a cocktail by its id. Admin only.

### Meal Plans

- `/api/mealPlan/create`: Create a meal plan. User only.
- `/api/mealPlan/getByDate/{userName}/{date}`: Get a meal plan by its user name and date. User only.
- `/api/mealPlan/getByDay/{userName}/{day}`: Get a meal plan by its user name and day. User only.
- `/api/mealPlan/getByUserName/{userName}`: Get a meal plan by its user name. User only.
- `/api/mealPlan/delete/{id}`: Delete a meal plan by its id. User only.
- `/api/mealPlan/update/{id}`: Update a meal plan by its id. User only.
- `/api/mealPlan/getAll`: Get all meal plans in the database. Admin only.

### User-related Endpoints

- `/api/user/getAll`: Get all users in the database. Admin only.
- `/api/user/getById/{id}`: Get a user by its id. Admin only.
- `/api/user/getByEmail/{email}`: Get a user by its email. Admin and user only.
- `/api/user/delete/{id}`: Delete a user by its id. Admin and user only.
- `/api/user/update/{id}`: Update a user by its id. Admin and user only.

### Authentication

- `/api/auth/register`: Register a new user.
- `/api/auth/login`: Login a user.
- `/api/auth/logout`: Logout a user. Admin and user only.
- `/api/auth/whoami`: Get the current user. Admin and user only.

###  Installation

Install HealthManager using one of the following methods:

**Build from source:**

1. Clone the HealthManager repository:
```sh
❯ git clone https://github.com/Tyna1992/HealthManager
```

2. Navigate to the project directory:
```sh
❯ cd HealthManager
```

3. Install the project dependencies:


**Using `npm`** &nbsp; [<img align="center" src="" />]()

```sh
❯ echo 'INSERT-INSTALL-COMMAND-HERE'
```


**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet restore
```


**Using `docker`** &nbsp; [<img align="center" src="https://img.shields.io/badge/Docker-2CA5E0.svg?style={badge_style}&logo=docker&logoColor=white" />](https://www.docker.com/)

```sh
❯ docker build -t Tyna1992/HealthManager .
```




###  Usage
Run HealthManager using the following command:
**Using `npm`** &nbsp; [<img align="center" src="" />]()

```sh
❯ echo 'INSERT-RUN-COMMAND-HERE'
```


**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet run
```


**Using `docker`** &nbsp; [<img align="center" src="https://img.shields.io/badge/Docker-2CA5E0.svg?style={badge_style}&logo=docker&logoColor=white" />](https://www.docker.com/)

```sh
❯ docker run -it {image_name}
```


###  Testing
Run the test suite using the following command:
**Using `npm`** &nbsp; [<img align="center" src="" />]()

```sh
❯ echo 'INSERT-TEST-COMMAND-HERE'
```


**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet test
```

