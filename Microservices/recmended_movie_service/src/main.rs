use rusqlite::{params, Connection, Result};
use rocket::{get, build, routes};
use rocket::serde::json::Json; // Now Rocket uses this for JSON
use serde::Serialize;
use std::error::Error;

/// This is the struct that will be used to store the data from the database
#[derive(Debug, Serialize, Clone)]
struct Movie {
    id: i32,
    date_added: String,
    genre_fk: i32,
    name: String,
    number_available: i32,
    number_in_stock: i32,
    release_date: String,
}

/// This function will save all the movies from the database into a vector
/// 
/// # Arguments
/// 
/// * `conn` - A connection to the database
/// 
/// # Returns
/// 
/// * `Vec<Movie>` - A vector of all the movies in the database
/// 
/// # Errors
/// 
/// * `Box<dyn Error>` - An error that can be any type of error
/// 
fn save_movies_to_vec(conn: &Connection) -> Result<Vec<Movie>, Box<dyn Error>> {
    let mut stmt = conn.prepare("SELECT * FROM Movies")?;
    let movies_iter = stmt.query_map(params![], |row| {
        Ok(Movie {
            id: row.get(0)?,
            date_added: row.get(1)?,
            genre_fk: row.get(2)?,
            name: row.get(3)?,
            number_available: row.get(4)?,
            number_in_stock: row.get(5)?,
            release_date: row.get(6)?,
        })
    })?;

    let mut movies = Vec::new();

    for movie in movies_iter {
        movies.push(movie?);
    }

    Ok(movies)
}

/// This route will return a random movie from the database
///
/// # Returns
/// 
/// * `Json<Movie>` - A JSON object of a random movie
/// 
#[get("/recommend_movie")]
async fn recommend_movie() -> Json<Movie> {
    let conn = Connection::open("../../VioRentalsData.db").unwrap();
    let movies_tab = save_movies_to_vec(&conn).unwrap();
    let url = format!("http://127.0.0.1:8000/random_number/{}/{}", 0, movies_tab.len());
    let body = reqwest::get(url).await.unwrap()
    .text()
    .await.unwrap();

    println!("{}", body);
    let body = body.parse::<usize>().unwrap();

    Json(movies_tab[body].clone())
}

/// This is the main function that will run the server
/// It will be listening on port 8001
/// 
#[rocket::main]
async fn main() {
    let _ = build()
        .mount("/", routes![recommend_movie])
        .launch()
        .await;
}
