using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Filters
{
    public class IdReservationExistsActionFilter : ActionFilterAttribute
    {
        public IEventReservationService _eventReservationService;

        public IdReservationExistsActionFilter(IEventReservationService eventReservationService, ICityEventService cityEventService)
        {
            _eventReservationService = eventReservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 404
            };

            long idReserva = (long)context.ActionArguments["id"];

            if (!_eventReservationService.GetEventReservation().Any(x => x.IdReservation == idReserva))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                problem.Title = "Reserva não existe";
                problem.Detail = "Não foi encontrada nenhuma reserva com o ID informado.";
                context.Result = new ObjectResult(problem);
            }

        }
    }
}
