﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LanguageExt.AspNetCore
{
    public static class Common
    {
        internal static IActionResult Ok(object result) => new OkObjectResult(result);
        internal static IActionResult Ok<T>(T t) => new OkObjectResult(t);
        internal static IActionResult NotFound() => new NotFoundResult();
        internal static IActionResult BadRequest<ERROR>(ERROR e) => new BadRequestObjectResult(e);
        internal static IActionResult BadRequest() => new BadRequestResult();
        internal static JsonResult OkJson<T>(T t) => new JsonResult(t);
        internal static JsonResult BadRequestJson<E>(E e) => JsonBadRequest(e);

        private static JsonResult JsonBadRequest<E>(E e) =>
            new JsonResult(e) { StatusCode = StatusCodes.Status400BadRequest };
    }
}
