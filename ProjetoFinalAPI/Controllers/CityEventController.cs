using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;
using ProjetoFinalAPI.Filters;

namespace ProjetoFinalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/CityEvent/GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEvent()
        {
            var cityEvent = _cityEventService.GetCityEvent;
            if (cityEvent == null)
                return NotFound("Não há clientes cadastrados.");
            return Ok(_cityEventService.GetCityEvent());
        }

        [HttpGet("/CityEvent/GetByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEventByTitle(string title)
        {
            var eventsList = _cityEventService.GetCityEventByTitle(title);
            if (!eventsList.Any())
                return NotFound();
            return Ok(eventsList);
        }

        [HttpGet("/CityEvent/GetByLocalAndDate/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEventByLocalAndDate(string local, DateTime dateEvent)
        {
            var eventsList = _cityEventService.GetCityEventByLocalAndDate(local, dateEvent);
            if (!eventsList.Any())
                return NotFound();
            return Ok(eventsList);
        }

        [HttpGet("/CityEvent/GetByPriceAndDate/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<CityEvent>> GetCityEventByPriceAndDate(decimal min, decimal max, DateTime dateEvent)
        {
            var eventsList = _cityEventService.GetCityEventByPriceAndDate(min, max, dateEvent);
            if (!eventsList.Any())
                return NotFound();
            return Ok(eventsList);
        }

        [HttpPost("/CityEvent/Insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles = "admin")]
        public ActionResult<CityEvent> InsertCityEvent(CityEvent cityEvent)
        {
            if (!_cityEventService.InsertCityEvent(cityEvent))
                return BadRequest();

            return CreatedAtAction(nameof(InsertCityEvent), cityEvent);
        }

        [HttpPut("/CityEvent/Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCityEvent(long id, CityEvent cityEvent)
        {
            if (!_cityEventService.UpdateCityEvent(id, cityEvent))
                return NotFound("Não foi possível realizar a atualização.");
            return NoContent();
        }

        [HttpDelete("/CityEvent/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public ActionResult<List<CityEvent>> DeleteCityEvent(long id)
        {
            if (!_cityEventService.DeleteCityEvent(id))
                return NotFound();
            return NoContent();
        }
    }
}