using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> GetCityEvent();
        List<CityEvent> GetCityEventByTitle(string title);
        List<CityEvent> GetCityEventByLocalAndDate(string local, DateTime dateHourEvent);
        List<CityEvent> GetCityEventByPriceAndDate(decimal min, decimal max, DateTime dateHourEvent);
        bool InsertCityEvent(CityEvent cityEvent);
        bool UpdateCityEvent(long idEvent, CityEvent cityEvent);
        bool DeleteCityEvent(long idEvent);

    }
}
