using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Success case is converted to 200 Ok. Fail is 400 Bad Request.
        /// </summary>
        /// <typeparam name="FAIL"></typeparam>
        /// <typeparam name="SUCCESS"></typeparam>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<FAIL, SUCCESS>(this Validation<FAIL, SUCCESS> validation) =>
            validation.Match(Ok, BadRequest);

        /// <summary>
        /// Success case is converted to 200 Ok. Fail is 400 Bad Request.
        /// </summary>
        /// <typeparam name="FAIL"></typeparam>
        /// <typeparam name="SUCCESS"></typeparam>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<FAIL, SUCCESS>(this Task<Validation<FAIL, SUCCESS>> validation) =>
            validation.Map(ToActionResult);

        /// <summary>
        /// Success case is converted to 200 Ok. Fail is 400 Bad Request.
        /// </summary>
        /// <typeparam name="FAIL"></typeparam>
        /// <typeparam name="SUCCESS"></typeparam>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<FAIL, SUCCESS>(this Validation<FAIL, Task<SUCCESS>> validation) =>
            validation.Sequence().ToActionResult();

        /// <summary>
        /// Success case is converted to 200 Ok. Fail is 400 Bad Request.
        /// </summary>
        /// <typeparam name="FAIL"></typeparam>
        /// <typeparam name="SUCCESS"></typeparam>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<FAIL, SUCCESS>(this Validation<Task<FAIL>, SUCCESS> validation) =>
            validation.MatchAsync(
                SuccAsync: a => Ok(a).AsTask(),
                FailAsync: async e => BadRequest(await e.SequenceParallel()));

        /// <summary>
        /// Success case is converted to 200 JsonResult. Fail is 400 Bad Request.
        /// </summary>
        /// <typeparam name="FAIL"></typeparam>
        /// <typeparam name="SUCCESS"></typeparam>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static JsonResult ToJsonResult<FAIL, SUCCESS>(this Validation<FAIL, SUCCESS> validation) =>
            validation.Match(OkJson, BadRequestJson);

        /// <summary>
        /// Success case is converted to 200 JsonResult. Fail is 400 Bad Request.
        /// </summary>
        /// <typeparam name="FAIL"></typeparam>
        /// <typeparam name="SUCCESS"></typeparam>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static Task<JsonResult> ToJsonResult<FAIL, SUCCESS>(this Task<Validation<FAIL, SUCCESS>> validation) =>
            validation.Map(ToJsonResult);
    }
}
