<div align="center">
<h1><b>Vio Rentals</b></h1>
<p><b>Movie rental platform designed with Clean Architecture</b></p>
</div>
<br/>

##### 🔽  TABLE OF CONTENTS  🔽
[1. Description](#desc)

[2. General features](#general-features)

[3. Build with](#build)

[4. Installation](#installation)

[5. Microservices](#microservices)

<a name="desc"></a>

## Description

Vio Rentals is the application based on [VioRentals - FilmsPlatform](https://github.com/KrzysztofJaronczyk/VioRentals-FilmsPlatform), designed to help manage your data such as customers, movies and more, including user authentication, CRUD operations and microservices.

<a name="features"></a>

## General features 

- User registration and login via Authentication API
- Search bar for movies and customers (in development)
- CRUD operations using API and dependency injection
- Graphical user interface
- Microservices like: "Lucky user" or "Recommended movie" made in Rust

<a name="build"></a>

## Build with

The project was designed in ASP.NET Clean Architecture divided into several parts

* **Infrastructure** is responsible for database context and repositories that allows operations on entities

* **Core** contains models and general application logic

* **Auth API** is responsible for a user authentication and authorization with JWT and a custom identity middleware

* **Web** contains a GUI for the application

The major frameworks/libraries used in the project:

* ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

* ![Rust](https://img.shields.io/badge/rust-%23000000.svg?style=for-the-badge&logo=rust&logoColor=white)

* ![TailwindCSS](https://img.shields.io/badge/tailwindcss-%2338B2AC.svg?style=for-the-badge&logo=tailwind-css&logoColor=white)

* ![SQLite](https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white)

<a name="installation"></a>

## Installation 

1. Download ZIP or clone repository
```bash
git clone https://github.com/jacobwawrzynski/VioRentals.git
```
2. Install rust
[Rust installer](https://www.rust-lang.org/tools/install)

3. Install Visual Studio with .NET 6
[Visual Studio Installer](https://visualstudio.microsoft.com/downloads/)

4. Make sure to also install C++ compiler, you can do this using Visual Studio Installer:

   ![image](https://github.com/jacobwawrzynski/VioRentals/blob/master/readme-images/cpp.png)

## Run the application

1. Run Microservices using start.bat file
2. Open the solution in Visual Studio using VioRentals.sln file
3. Run Auth.API if you want to test just functionality of the appliction
4. To run the whole application - right click solution file in solution explorer (VioRentals.sln) and choose Configure Startup Project
5. Choose Multiple Startup Projects and <ins>**make sure to set Auth.API as first and Web as second like this**</ins>:
   
   ![image](https://github.com/jacobwawrzynski/VioRentals/blob/master/readme-images/startup.png)


## Microservices

### Description

Microservices are written in Rust and are responsible for the following tasks:

* **Random Number Service** - returns a random number from range put in the request get parameters

* **Lucky User Service** - returns a random user from the database (use random number service to get a random number and then get a user)

* **Recommended Movie Service** - returns a recommended movie from the database (use random number service to get a random number and then get a movie and send movie as "recommended")

### Endpoints

* **Random Number Service** - http://localhost:8000/random_number/from/to - where "from" and "to" are integers

* **Recommended Movie Service** - http://localhost:8001/recommend_movie

* **Lucky User Service** - http://localhost:8002/lucky_user

### Docs

Run docs.bat to generate docs for microservices

## License

[MIT](https://choosealicense.com/licenses/mit/)
