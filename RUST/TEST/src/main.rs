use postgres::{Client, Error, NoTls,};
use bigdecimal::BigDecimal;

struct Film {
    film_id: i32,
    title: String,
    description: String,
    release_year: i32,
    language_id: i16,
    rental_duration: i16,
    rental_rate: bigdecimal::BigDecimal,
    length: i32,
    replacement_cost: f32,
    rating: String,
    last_update: String, // TO DATE
    special_features: String,
    fulltext: String 
}

fn main() {
    let url = "postgresql://postgres:admin@localhost:5432/dvdrental";
    let mut client = Client::connect(url, NoTls).unwrap();

    for row in client.query("SELECT * FROM film", &[]).unwrap() {
        let film = Film {
            film_id: row.get(0),
            title: row.get(1),
            description: row.get(2),
            release_year: row.get(3),
            language_id: row.get(4),
            rental_duration: row.get(5),
            rental_rate: row.get(6),
            length: row.get(7),
            replacement_cost: row.get(8),
            rating: row.get(9),
            last_update: row.get(10),
            special_features: row.get(11),
            fulltext: row.get(12),
        };
        println!("фильм: {}", film.title);
    }

    println!("Hello, world!");
}
