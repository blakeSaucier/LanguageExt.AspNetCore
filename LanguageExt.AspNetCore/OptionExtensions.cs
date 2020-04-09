using Microsoft.AspNetCore.Mvc;
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
        /// Some case is converted to 200 Ok. None case is 404.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option) =>
            option.Map(ToActionResult);

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
    }
}
