<div align="center">
<h1><b>Vio Rentals</b></h1>
<p><b>Movie rental platform designed with Clean Architecture</b></p>
</div>
<br/>

##### 🔽  TABLE OF CONTENTS  🔽
[1. Description](#desc)

[2. Features](#features)

[3. Build with](#build)

[4. Installation](#installation)

<a name="desc"></a>

## Description

Vio Rentals is the application based on [VioRentals - FilmsPlatform](https://github.com/KrzysztofJaronczyk/VioRentals-FilmsPlatform), designed to help manage your data such as customers, movies and more, including user authentication, CRUD operations and mocroservices.

<a name="features"></a>

## Features 

- User registration and login via Authentication API
- Search bar for movies and customers
- CRUD operations via dependency injection
- ...

<a name="build"></a>

## Build with

The project is designed with ASP.NET Clean Architecture devided into several parts

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
