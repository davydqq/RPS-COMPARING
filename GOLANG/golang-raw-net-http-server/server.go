package main

import (
	"database/sql"
	"encoding/json"
	"fmt"
	"net/http"

	_ "github.com/lib/pq"
)

type film struct {
	Film_id          int
	Title            string
	Description      string
	Release_year     int
	Language_id      int
	Rental_duration  int
	Rental_rate      float32
	Length           int
	Replacement_cost float32
	Rating           string
	Last_update      string
	Special_features string
	Fulltext         string
}

func main() {
	connStr := "user=postgres password=admin dbname=dvdrental sslmode=disable"

	db, err := sql.Open("postgres", connStr)
	if err != nil {
		panic(err)
	}

	http.HandleFunc("/films/all/raw", func(w http.ResponseWriter, r *http.Request) {

		var query = "select * from film"
		// if only one expected
		take := r.URL.Query().Get("take")
		if take != "" {
			query = "select * from film LIMIT " + take
		}

		rows, err := db.Query(query)
		if err != nil {
			panic(err)
		}
		defer rows.Close()

		films := []film{}

		for rows.Next() {
			p := film{}
			err := rows.Scan(&p.Film_id, &p.Title, &p.Description, &p.Release_year, &p.Language_id, &p.Rental_duration, &p.Rental_rate, &p.Length, &p.Replacement_cost,
				&p.Rating, &p.Last_update, &p.Special_features, &p.Fulltext)
			if err != nil {
				fmt.Println(err)
				continue
			}
			films = append(films, p)
		}

		res2B, err := json.Marshal(films)
		if err != nil {
			panic(err)
		}

		fmt.Fprintf(w, string(res2B))
	})

	http.HandleFunc("/text", func(w http.ResponseWriter, r *http.Request) {
		fmt.Fprint(w, "TEST")
	})

	fmt.Println("Server is listening...")
	http.ListenAndServe("localhost:8181", nil)
}
