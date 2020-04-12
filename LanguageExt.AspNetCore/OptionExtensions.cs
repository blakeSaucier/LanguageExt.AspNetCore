using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class OptionExtensions
    {
        /// <summary>
        /// Some case is converted to 200 Ok. None case is 404.
        /// </summary>
        /// <typeparam name="T"></typeparam>s
        /// <param name="option"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<T>(this Option<T> option) =>
            option.Match(Ok, NotFound);

        /// <summary>
        /// By default, Some case is converted to 200 OK and None is 404.
        /// Optionally, provide functions to handle Some and None cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<T>(this Option<T> option, Func<T, IActionResult> some = null, Func<IActionResult> none = null) =>
            option.Match(some ?? Ok, none ?? NotFound);

        /// <summary>
        /// Some case is converted to 200 Ok. None case is 404.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option) =>
            option.Map(ToActionResult);

        /// <summary>
        /// By default, Some case is converted to 200 OK and None is 404.
        /// Optionally, provide functions to handle Some and None cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option, Func<T, IActionResult> some = null, Func<IActionResult> none = null) =>
            option.Map(o => o.ToActionResult(some, none));

        /// <summary>
        /// Some case is converted to Ok JsonResult. None case is 404.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IActionResult ToJsonResult<T>(this Option<T> option) =>
            option.Match(OkJson, NotFound);
        
        /// <summary>
        /// Some case is converted to Ok JsonResult. None case is 404.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToJsonResult<T>(this Task<Option<T>> option) =>
            option.Map(ToJsonResult);

        /// <summary>
        /// Some case is converted to Ok JsonResult. None case is 404.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this OptionAsync<T> option) =>
            option.Match(Ok, NotFound);

        /// <summary>
        /// By default, Some case is converted to 200 OK and None is 404.
        /// Optionally, provide functions to handle Some and None cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this OptionAsync<T> option, Func<T, IActionResult> some = null, Func<IActionResult> none = null) =>
            option.Match(some ?? Ok, none ?? NotFound);
    }
}
