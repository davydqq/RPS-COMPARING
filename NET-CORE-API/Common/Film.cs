namespace Common
{
    public class Film
    {
        public int Film_id { set; get; }

        public string? Title { set; get; }

        public string? Description { set; get; }

        public int Release_year { set; get; }

        public int Language_id { set; get; }

        public int Rental_duration { set; get; }

        public double Rental_rate { set; get; }

        public int Length { set; get; } 

        public double Replacement_cost { set; get; }

        public string Rating { set; get; }

        public DateTimeOffset Last_update { set; get; }

        public string[] Special_features { set; get; }

        public dynamic Fulltext { set; get; }


        public Film Init(int film_id, string title, string description, int release_year, int language_id, 
            int rental_duration, double rental_rate, int length, double replacement_cost, string rating, 
            DateTimeOffset last_update, string[] special_features, dynamic fulltext)
        {
            Film_id = film_id;
            Title = title;
            Description = description;
            Release_year = release_year;
            Language_id = language_id;
            Rental_duration = rental_duration;
            Rental_rate = rental_rate;
            Length = length;
            Replacement_cost = replacement_cost;
            Rating = rating;
            Last_update = last_update;
            Special_features = special_features;
            Fulltext = fulltext;

            return this;
        }
    }
}