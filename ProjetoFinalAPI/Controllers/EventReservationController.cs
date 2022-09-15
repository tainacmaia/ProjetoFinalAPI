using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            Console.WriteLine("Instanciando EventReservation Controller");
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/EventReservation/GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<EventReservation>> GetEventReservation()
        {
            var eventReservation = _eventReservationService.GetEventReservation;
            if (eventReservation == null)
                return NotFound("Não há reservas cadastradas.");
            return Ok(_eventReservationService.GetEventReservation());
        }

        [HttpGet("/EventReservation/GetByPersonNameAndTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<EventReservation> GetEventReservationByPersonNameAndTitle(string personName, string title)
        {
            var eventReservation = _eventReservationService.GetEventReservationByPersonNameAndTitle(personName, title);
            if (eventReservation == null)
                return NotFound();
            return Ok(eventReservation);
        }

        [HttpPost("/EventReservation/Insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<EventReservation> InsertEventReservation(EventReservation eventReservation)
        {
            if (!_eventReservationService.InsertEventReservation(eventReservation))
                return BadRequest();

            return CreatedAtAction(nameof(InsertEventReservation), eventReservation);
        }

        [HttpPut("/EventReservation/Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEventReservation(long id, EventReservation eventReservation)
        {
            if (!_eventReservationService.UpdateEventReservation(id, eventReservation))
                return NotFound("Reserva não encontrada.");

            return NoContent();
        }

        [HttpDelete("/EventReservation/Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public ActionResult<List<EventReservation>> DeleteEventReservation(long id)
        {
            if (!_eventReservationService.DeleteEventReservation(id))
                return NotFound("Reserva não encontrada.");
            return NoContent();
        }
    }
}
