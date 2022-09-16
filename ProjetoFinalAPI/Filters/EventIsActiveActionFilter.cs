using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Filters
{
    public class EventIsActiveActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public EventIsActiveActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var eventReservation = context.ActionArguments["eventReservation"] as EventReservation;
            var verifyEvent = _cityEventService.GetCityEvent().Find(x => x.IdEvent == eventReservation.IdEvent);
            var problem = new ProblemDetails();
            if (verifyEvent.Status == false)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
                problem.Status = 409;
                problem.Title = "Evento inativo";
                problem.Detail = "Não foi possível realizar a reserva pois o evento está inativo.";
                context.Result = new ObjectResult(problem);
            }
        }
    }
}
