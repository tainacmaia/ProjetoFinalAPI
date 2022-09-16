using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetEventReservation();
        List<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title);
        bool InsertEventReservation(EventReservation eventReservation);
        bool UpdateEventReservation(long reservationId, long quantity);
        bool DeleteEventReservation(long idEventReservation);
    }
}
