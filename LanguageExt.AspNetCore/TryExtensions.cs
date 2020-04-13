using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class TryExtensions
    {
        /// <summary>
        /// Success case is converted to 200 OK.
        /// Exception is converted to 500 Server Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<T>(this Try<T> @try) =>
            @try.Match(Ok, ServerError);

        /// <summary>
        /// By default, Success case is converted to 200 OK and an Exception is converted to 500 Server Error.
        /// Optionally, provide mapping functions for Success and Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<T>(this Try<T> @try, Func<T, IActionResult> success = null, Func<Exception, IActionResult> fail = null) =>
            @try.Match(success ?? Ok, fail ?? ServerError);

        /// <summary>
        /// Success case is converted to 200 OK.
        /// Exception is converted to 500 Server Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try) =>
            @try.Match(Ok, ServerError);

        /// <summary>
        /// By default, Success case is converted to 200 OK and an Exception is converted to 500 Server Error.
        /// Optionally, provide mapping functions for Success and Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try, Func<T, IActionResult> success = null, Func<Exception, IActionResult> fail = null) =>
            @try.Match(success ?? Ok, fail ?? ServerError);

        /// <summary>
        /// Success case is converted to 200 OK.
        /// Exception is converted to 500 Server Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try) =>
            @try.Match(Ok, ServerError);

        /// <summary>
        /// By default, Success case is converted to 200 OK and an Exception is converted to 500 Server Error.
        /// Optionally, provide mapping functions for Success and Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="success"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try, Func<T, IActionResult> success = null, Func<Exception, IActionResult> fail = null) =>
            @try.Match(success ?? Ok, fail ?? ServerError);

        /// <summary>
        /// Success case is converted to 200 OK.
        /// Exception is converted to 500 Server Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<T>(this Try<Task<T>> @try) =>
            @try.ToAsync().ToActionResult();
    }
}
