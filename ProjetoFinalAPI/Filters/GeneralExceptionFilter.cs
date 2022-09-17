using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace ProjetoFinalAPI.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Erro Inesperado",
                Detail = "Ocorreu um erro inesperado na solicitação.",
                Type = context.Exception.GetType().Name
            };

            switch (context.Exception)
            {
                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    problem.Status = 503;
                    problem.Title = "Erro inesperado ao se comunicar com o banco de dados.";
                    context.Result = new ObjectResult(problem);
                    break;
                case NullReferenceException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    problem.Status = 417;
                    problem.Title = "Erro inesperado no sistema.";
                    context.Result = new ObjectResult(problem);
                    break;

                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    problem.Status = 500;
                    problem.Title = "Erro inesperado. Tente novamente.";
                    context.Result = new ObjectResult(problem);
                    break;
            }

            Console.WriteLine($"Tipo da exceção: {context.Exception.GetType().Name};\nMensagem: {context.Exception.Message};\nStack Trace {context.Exception.StackTrace};");
        }
    }
}
