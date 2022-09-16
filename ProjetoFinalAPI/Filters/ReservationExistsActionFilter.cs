using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Filters
{
    public class ReservationExistsActionFilter : ActionFilterAttribute
    {
        public IEventReservationService _eventReservationService;
        public ICityEventService _cityEventService;

        public ReservationExistsActionFilter(IEventReservationService eventReservationService, ICityEventService cityEventService)
        {
            _eventReservationService = eventReservationService;
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 404
            };

            var eventReservation = context.ActionArguments["eventReservation"] as EventReservation;

            if (HttpMethods.IsPost(context.HttpContext.Request.Method) &&
                !_cityEventService.GetCityEvent().Any(x => x.IdEvent == eventReservation.IdEvent))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                problem.Title = "Evento não existe";
                problem.Detail = "Não foi encontrado nenhum evento com o ID informado.";
                context.Result = new ObjectResult(problem);
            }
        }
    }
}
