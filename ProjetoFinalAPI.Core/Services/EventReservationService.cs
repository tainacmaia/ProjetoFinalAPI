using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public List<EventReservation> GetEventReservation()
        {
            return _eventReservationRepository.GetEventReservation();
        }

        public List<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title)
        {
            return _eventReservationRepository.GetEventReservationByPersonNameAndTitle(personName, title);
        }

        public bool InsertEventReservation(EventReservation eventReservation)
        {
            return _eventReservationRepository.InsertEventReservation(eventReservation);
        }
        public bool UpdateEventReservation(long reservationId, long quantity)
        {

            return _eventReservationRepository.UpdateEventReservation(reservationId, quantity);
        }
        public bool DeleteEventReservation(long idReservation)
        {
            return _eventReservationRepository.DeleteEventReservation(idReservation);
        }
    }
}
