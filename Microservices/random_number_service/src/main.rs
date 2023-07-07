use rocket::{get, build, routes};
use rand::Rng;

/// This function will return a random number between the given range
/// 
/// # Arguments
/// 
/// * `from` - The lower bound of the range
/// * `to` - The upper bound of the range
/// 
/// # Returns
/// 
/// * `String` - The random number
///
#[get("/random_number/<from>/<to>")]
fn random_number(from: i32, to: i32) -> String {
    let from: i64 = from.into();
    let to: i64 = to.into();
    let mut rng = rand::thread_rng();
    let random_number = rng.gen_range(from..to);

    random_number.to_string()
}

/// This is the main function that will run the server
/// It will be listening on port 8000
/// 
#[rocket::main]
async fn main() {
    let _ = build()
        .mount("/", routes![random_number])
        .launch()
        .await;
}
