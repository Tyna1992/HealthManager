# HealthManager Documentation

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