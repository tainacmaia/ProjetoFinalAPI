using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Core.Services
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public IEventReservationRepository _eventReservationRepository;

        public CityEventService(ICityEventRepository cityEventRepository, IEventReservationRepository eventReservationRepository)
        {
            _cityEventRepository = cityEventRepository;
            _eventReservationRepository = eventReservationRepository;
        }

        public List<CityEvent> GetCityEvent()
        {
            return _cityEventRepository.GetCityEvent();
        }

        public List<CityEvent> GetCityEventByTitle(string title)
        {
            return _cityEventRepository.GetCityEventByTitle(title);
        }
        public List<CityEvent> GetCityEventByLocalAndDate(string local, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetCityEventByLocalAndDate(local, dateHourEvent);
        }
        public List<CityEvent> GetCityEventByPriceAndDate(decimal min, decimal max, DateTime dateHourEvent)
        {
            return _cityEventRepository.GetCityEventByPriceAndDate(min, max, dateHourEvent);
        }

        public bool InsertCityEvent(CityEvent cityEvent)
        {
            return _cityEventRepository.InsertCityEvent(cityEvent);
        }
        public bool UpdateCityEvent(long idEvent, CityEvent cityEvent)
        {
            //try
            //{
            //    cityEvent = null;
            //    cityEvent.IdEvent = idEvent;
            //}
            //catch (Exception ex)
            //{
            //    var tipoExcecao = ex.GetType().Name;
            //    var mensagem = ex.Message;
            //    var caminho = ex.InnerException.StackTrace;

            //    Console.WriteLine($"Tipo da Exceção: {tipoExcecao}, Mensagem: {mensagem}, Stack Trace: {caminho}");
            //    return false;
            //}

            return _cityEventRepository.UpdateCityEvent(idEvent, cityEvent);
        }

        public bool DeleteCityEvent(long idEvent)
        {
            var reservationList = _eventReservationRepository.GetEventReservation().ToList();
            if (!reservationList.Any(x => x.IdEvent == idEvent))
                return _cityEventRepository.DeleteCityEvent(idEvent);
            var cityEventList = _cityEventRepository.GetCityEvent().ToList();
            var deactivate = cityEventList.FirstOrDefault(x => x.IdEvent == idEvent);
            deactivate.Status = false;
            return _cityEventRepository.UpdateCityEvent(idEvent, deactivate);
        }
    }
}
