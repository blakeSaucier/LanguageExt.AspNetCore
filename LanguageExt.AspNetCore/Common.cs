using Microsoft.AspNetCore.Mvc;

namespace LanguageExt.AspNetCore
{
    public static class Common
    {
        internal static IActionResult Ok(object result) => new OkObjectResult(result);
        internal static IActionResult Ok<T>(T t) => new OkObjectResult(t);
        internal static IActionResult NotFound() => new NotFoundResult();
        internal static JsonResult OkJson<T>(T t) => new JsonResult(t);
    }
}
