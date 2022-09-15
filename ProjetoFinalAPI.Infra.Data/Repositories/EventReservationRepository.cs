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

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }

        public List<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title)
        {
            var query = @$"SELECT * FROM EventReservation AS e INNER JOIN CityEvent AS c ON
                        e.PersonName = @PersonName AND c.Title LIKE ('%' + @Title '%') AND 
                        e.IdEvent = c.IdEvent ";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters(new 
            { 
                title, 
                personName 
            });
            return conn.Query<EventReservation>(query, parameters).ToList();
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

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
        public bool UpdateEventReservation(long reservationId, EventReservation eventReservation)
        {
            var query = "UPDATE EventReservation SET Quantity = @Quantity WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters(new
            {
                eventReservation.Quantity
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
        public bool DeleteEventReservation(long idReservation)
        {
            var query = "DELETE FROM EventReservation WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters(new
            {
                idReservation
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
