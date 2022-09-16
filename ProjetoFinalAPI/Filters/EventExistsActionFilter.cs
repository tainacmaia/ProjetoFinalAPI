using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoFinalAPI.Core.Interfaces;
using ProjetoFinalAPI.Core.Models;

namespace ProjetoFinalAPI.Filters
{
    public class EventExistsActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public EventExistsActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var problem = new ProblemDetails();
            var cityEvent = context.ActionArguments["cityEvent"] as CityEvent;
            if (_cityEventService.GetCityEvent().Any(x => (x.Title == cityEvent.Title &&
                                                           x.DateHourEvent == cityEvent.DateHourEvent &&
                                                           x.Local == cityEvent.Local)))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status409Conflict);
                problem.Status = 409;
                problem.Title = "Duplicação de informação";
                    if (HttpMethods.IsPost(context.HttpContext.Request.Method))
                        problem.Detail = "Já existe um evento com as informações dadas.";
                    if (HttpMethods.IsPut(context.HttpContext.Request.Method))
                        problem.Detail = "A descrição dada é exatamente igual à já cadastrada.";
                context.Result = new ObjectResult(problem);
            }
            if (HttpMethods.IsPut(context.HttpContext.Request.Method) &&
                !_cityEventService.GetCityEvent().Any(x => x.IdEvent == cityEvent.IdEvent))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                problem.Status = 404;
                problem.Title = "Evento não existe";
                problem.Detail = "Não foi encontrado nenhum evento com o ID informado.";
                context.Result = new ObjectResult(problem);
            }

        }
    }
}
