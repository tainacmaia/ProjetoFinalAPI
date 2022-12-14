using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Infra.Data.Repositories
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<CityEvent> GetCityEvent()
        {
            var query = "SELECT*FROM CityEvent";
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> GetCityEventByTitle(string title)
        {

            var query = $"SELECT * FROM CityEvent WHERE Title LIKE ('%' +  @Title + '%') ";
            var parameters = new DynamicParameters(new
            {
                title
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<CityEvent> GetCityEventByLocalAndDate(string local, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE Local LIKE ('%'+ @Local + '%') AND CONVERT(DATE, DateHourEvent)= @DateHourEvent";
            var parameters = new DynamicParameters(new
            {
                local,
                dateHourEvent
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }

        }

        public List<CityEvent> GetCityEventByPriceAndDate(decimal min, decimal max, DateTime dateHourEvent)
        {
            var query = "SELECT * FROM CityEvent WHERE Price >= @min AND Price <= @max AND CONVERT(DATE, DateHourEvent)= @DateHourEvent";
            var parameters = new DynamicParameters(new
            {
                min,
                max,
                dateHourEvent
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<CityEvent>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {

            var query = "INSERT INTO CityEvent VALUES(@Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,
                cityEvent.Status
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }
        public bool UpdateCityEvent(CityEvent cityEvent)
        {
            var query = "UPDATE CityEvent SET Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, " +
                "Local = @Local, Address = @Address, Price = @Price, Status = @Status WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters(cityEvent);

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }
        public bool DeleteCityEvent(long idEvent)
        {
            var query = "DELETE FROM CityEvent WHERE IdEvent = @IdEvent";

            var parameters = new DynamicParameters(new
            {
                idEvent
            });

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Execute(query, parameters) == 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }
    }
}
