using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Dynamic;

namespace NET_CORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        const string dbConnection = "Host=localhost;Port=5432;Database=dvdrental;Username=postgres;Password=admin";

        [HttpGet("npgsql")]
        public async Task<List<Film>> GetAllRaw([FromQuery] int? take)
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

        [HttpGet("npgsql/dynamic")]
        public async Task<List<dynamic>> GetAllRawDynamic([FromQuery] int? take)
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

            var result = new List<dynamic>();
            while (await rdr.ReadAsync())
            {
                var special_features = rdr.GetValue(11);
                var fulltext = rdr.GetValue(12);

                dynamic film = new ExpandoObject();

                film.film_id = rdr.GetValue(0);
                film.title = rdr.GetValue(1);
                film.description = rdr.GetValue(2);
                film.release_year = rdr.GetValue(3);
                film.language_id = rdr.GetValue(4);
                film.rental_duration = rdr.GetValue(5);
                film.rental_rate = rdr.GetValue(6);
                film.length = rdr.GetValue(7);
                film.replacement_cost = rdr.GetValue(8);
                film.rating = rdr.GetValue(9);
                film.last_update = rdr.GetValue(10);
                film.special_features = special_features;
                film.fulltext = fulltext;

                result.Add(film);
            }

            // TODO MAYBE CLOSE

            return result;
        }

        [HttpGet("ef-core")]
        public async Task<List<Film>> GetAllEfCore([FromQuery] int? take)
        {
            return null;
        }

        [HttpGet("dapper")]
        public async Task<List<Film>> GetAllDapper([FromQuery] int? take)
        {
            return null;
        }
    }
}
