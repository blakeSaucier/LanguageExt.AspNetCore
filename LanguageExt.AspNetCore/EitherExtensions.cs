using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class EitherExtensions
    {
        /// <summary>
        /// Right case is converted to 200 OK.
        /// Left case is serialized as a 500 server error.
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<L, R>(this Either<L, R> either) =>
            either.Match(Ok, ServerError);

        /// <summary>
        /// Right case is converted to 200 OK.
        /// By default, Left case is returned as 500 server error.
        /// Alternatively, provide a function to handle the Left case
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<L, R>(this Either<L, R> either, Func<L, IActionResult> left = null)
            => either.Match(Ok, left ?? ServerError);

        /// <summary>
        /// By default, Right case is converted to 200 OK.
        /// By default, Left case is returned as 500 server error.
        /// Alternatively, provide a functions to handle the Left and Right cases.
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult<L, R>(this Either<L, R> either, Func<L, IActionResult> left = null, Func<R, IActionResult> right = null) =>
            either.Match(right ?? Ok, left ?? ServerError);
        
        /// <summary>
        /// Right case is converted to 200 OK.
        /// Left case is serialized as a 500 server error.
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either) =>
            either.Map(ToActionResult);

        /// <summary>
        /// Right case is converted to 200 OK.
        /// By default, Left case is returned as 500 server error.
        /// Alternatively, provide a function to handle the Left case
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<L, IActionResult> left = null)
            => either.Map(v => v.ToActionResult(left));

        /// <summary>
        /// By default, Right case is converted to 200 OK.
        /// By default, Left case is returned as 500 server error.
        /// Alternatively, provide a functions to handle the Left and Right cases.
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static Task<IActionResult> ToActionResult<L, R>(this Task<Either<L, R>> either, Func<L, IActionResult> left = null, Func<R, IActionResult> right = null) =>
            either.Map(v => v.ToActionResult(left, right));

        public static Task<IActionResult> ToActionResult<L, R>(this EitherAsync<L, R> either) =>
            either.Match(Ok, ServerError);
    }
}
