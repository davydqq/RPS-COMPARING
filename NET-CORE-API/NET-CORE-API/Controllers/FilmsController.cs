using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace NET_CORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        const string dbConnection = "Host=localhost;Port=5432;Database=dvdrental;Username=postgres;Password=admin";

        [HttpGet("npgsql")]
        public async Task<List<Film>> GetAll([FromQuery] int? take)
        {
            using var con = new NpgsqlConnection(dbConnection);
            await con.OpenAsync();

            string sql = $"SELECT * FROM {nameof(Film)}";

            if (take.HasValue)
            {
                sql = $"SELECT * FROM {nameof(Film)} LIMIT {take.Value}";
            }

            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();

            var result = new List<Film>();
            while (await rdr.ReadAsync())
            {
                var special_features = rdr.GetValue(11) as string[];
                var fulltext = rdr.GetValue(12);

                var film = new Film().Init(
                    film_id: rdr.GetInt32(0), 
                    title: rdr.GetString(1), 
                    description: rdr.GetString(2), 
                    release_year: rdr.GetInt32(3), 
                    language_id: rdr.GetInt32(4), 
                    rental_duration: rdr.GetInt32(5), 
                    rental_rate: rdr.GetDouble(6),
                    length: rdr.GetInt32(7), 
                    replacement_cost: rdr.GetDouble(8),
                    rating: rdr.GetString(9), 
                    last_update: rdr.GetDateTime(10),
                    special_features: special_features,
                    fulltext: fulltext
                    );
                result.Add(film);
            }

            // TODO MAYBE CLOSE

            return result;
        }
    }
}
