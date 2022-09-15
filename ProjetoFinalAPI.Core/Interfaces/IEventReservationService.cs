using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Core.Interfaces
{
    public interface IEventReservationService
    {
        List<EventReservation> GetEventReservation();
        List<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title);
        bool InsertEventReservation(EventReservation eventReservation);
        bool UpdateEventReservation(long eventReservationId, EventReservation eventReservation);
        bool DeleteEventReservation(long eventReservationId);
    }
}
