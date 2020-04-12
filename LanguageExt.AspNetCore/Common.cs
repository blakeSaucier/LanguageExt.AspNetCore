using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LanguageExt.AspNetCore
{
    public static class Common
    {
        internal static IActionResult Ok<T>(T t) => (t is Unit) ? NoContent() : OkResult(t);
        
        internal static IActionResult NotFound() => new NotFoundResult();
        
        internal static IActionResult BadRequest<ERROR>(ERROR e) => new BadRequestObjectResult(e);
        
        internal static IActionResult BadRequest() => new BadRequestResult();
        
        internal static IActionResult ServerError<E>(E e) => Error(e);
        
        internal static IActionResult ServerError() => Error();

        internal static IActionResult NoContent() => new NoContentResult();
        
        internal static JsonResult OkJson<T>(T t) => OkJsonResult(t);
        
        internal static JsonResult BadRequestJson<E>(E e) => BadRequestJsonResult(e);

        static IActionResult OkResult<T>(T t) => new OkObjectResult(t);

        static JsonResult OkJsonResult<T>(T t) =>
            new JsonResult(t) { StatusCode = StatusCodes.Status200OK };
        
        static JsonResult BadRequestJsonResult<E>(E e) =>
            new JsonResult(e) { StatusCode = StatusCodes.Status400BadRequest };
        
        static IActionResult Error() => new StatusCodeResult(StatusCodes.Status500InternalServerError);
        
        static IActionResult Error<E>(E e) =>
            new ObjectResult(e) { StatusCode = StatusCodes.Status500InternalServerError };
    }
}
