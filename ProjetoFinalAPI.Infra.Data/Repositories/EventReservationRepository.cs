using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Infra.Data.Repositories
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<EventReservation> GetEventReservation()
        {
            var query = "SELECT*FROM EventReservation";

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<EventReservation>(query).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public List<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title)
        {
            var query = @$"SELECT * FROM EventReservation e INNER JOIN CityEvent c ON
                        e.PersonName = @PersonName AND c.Title LIKE ('%' + @Title + '%')
                        WHERE e.IdEvent = c.IdEvent";
            
            var parameters = new DynamicParameters(new 
            { 
                title, 
                personName 
            });
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                return conn.Query<EventReservation>(query, parameters).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao comunicar com banco, mensagem {ex.Message}, stack trace {ex.StackTrace}");
                throw;
            }
        }

        public bool InsertEventReservation(EventReservation eventReservation)
        {
            var query = "INSERT INTO EventReservation VALUES(@IdEvent, @PersonName, @Quantity)";

            var parameters = new DynamicParameters(new
            {
                eventReservation.IdEvent,
                eventReservation.PersonName,
                eventReservation.Quantity
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
        public bool UpdateEventReservation(long idReservation, long quantity)
        {
            var query = "UPDATE EventReservation SET Quantity = @Quantity WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters(new
            {
                idReservation,
                quantity
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
        public bool DeleteEventReservation(long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters(new
            {
                idReservation
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
