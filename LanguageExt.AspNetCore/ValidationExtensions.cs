using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class ValidationExtensions
    {
        public static IActionResult ToActionResult<FAIL, SUCCESS>(this Validation<FAIL, SUCCESS> validation) =>
            validation.Match(Ok, BadRequest);

        public static Task<IActionResult> ToActionResult<FAIL, SUCCESS>(this Task<Validation<FAIL, SUCCESS>> validation) =>
            validation.Map(ToActionResult);

        public static Task<IActionResult> ToActionResult<FAIL, SUCCESS>(this Validation<FAIL, Task<SUCCESS>> validation) =>
            validation.Sequence().ToActionResult();

        public static Task<IActionResult> ToActionResult<FAIL, SUCCESS>(this Validation<Task<FAIL>, SUCCESS> validation) =>
            validation.MatchAsync(
                SuccAsync: a => Ok(a).AsTask(),
                FailAsync: async e => BadRequest(await e.Sequence()));

        public static JsonResult ToJsonResult<FAIL, SUCCESS>(this Validation<FAIL, SUCCESS> validation) =>
            validation.Match(OkJson, BadRequestJson);
        
        public static Task<JsonResult> ToJsonResult<FAIL, SUCCESS>(this Task<Validation<FAIL, SUCCESS>> validation) =>
            validation.Map(ToJsonResult);
    }
}
