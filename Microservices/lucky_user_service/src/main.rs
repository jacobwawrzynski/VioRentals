use rusqlite::{params, Connection, Result};
use rocket::{get, build, routes};
use rocket::serde::json::Json;
use serde::Serialize;
use std::error::Error;

/// This is the struct that will be used to store the data from the database
#[derive(Debug, Serialize, Clone)]
struct User {
    id: i32,
    email: String,
    password_hash: Vec<u8>,
    password_salt: Vec<u8>,
    forname: String,
    lastname: String,
}

/// This function will save all the users from the database into a vector
/// 
/// # Arguments
/// 
/// * `conn` - A connection to the database
/// 
/// # Returns
/// 
/// * `Result<Vec<User>, Box<dyn Error>>` - A vector of users
/// 
fn save_users_to_vec(conn: &Connection) -> Result<Vec<User>, Box<dyn Error>> {
    let mut stmt = conn.prepare("SELECT * FROM Users")?;
    
    let user_iter = stmt.query_map(params![], |row| {
        Ok(User {
            id: row.get(0)?,
            email: row.get(1)?,
            password_hash: row.get(2)?,
            password_salt: row.get(3)?,
            forname: row.get(4)?,
            lastname: row.get(5)?,
        })
    })?;

    let mut users_tab = Vec::new();

    for user in user_iter {
        users_tab.push(user?);
    }

    Ok(users_tab)
}

/// This function will return a lucky user from the database
///
/// # Returns
/// 
/// * `Json<User>` - A JSON object of a random movie
/// 
#[get("/lucky_user")]
async fn lucky_user() -> Json<User> {
    let conn = Connection::open("../../VioRentalsData.db").unwrap();
    let users_tab = save_users_to_vec(&conn).unwrap();
    let url = format!("http://127.0.0.1:8000/random_number/{}/{}", 0, users_tab.len());
    let body = reqwest::get(url).await.unwrap()
    .text()
    .await.unwrap();

    let body = body.parse::<usize>().unwrap();

    Json(users_tab[body].clone())
}

/// This is the main function that will run the server
/// It will be listening on port 8002
/// 
#[rocket::main]
async fn main() {
    let _ = build()
        .mount("/", routes![lucky_user])
        .launch()
        .await;
}
