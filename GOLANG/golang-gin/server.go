package main

import (
	"database/sql"
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
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
	// INIT DB

	connStr := "user=postgres password=admin dbname=dvdrental sslmode=disable"
	db, err := sql.Open("postgres", connStr)
	if err != nil {
		panic(err)
	}

	r := gin.Default()
	r.GET("/text", func(c *gin.Context) {
		c.String(http.StatusOK, fmt.Sprintf("TEST"))
	})
	r.GET("/raw", func(c *gin.Context) {
		var query = "select * from film"
		// if only one expected
		take := c.Request.URL.Query().Get("take")
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

		c.JSON(http.StatusOK, gin.H{
			"status": "posted",
			"films":  films,
		})
	})
	r.Run() // listen and serve on 0.0.0.0:8080 (for windows "localhost:8080")
}
