using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class TryExtensions
    {
        public static IActionResult ToActionResult<T>(this Try<T> @try) =>
            @try.Match(Ok, ServerError);
        
        public static Task<IActionResult> ToActionResult<T>(this Task<Try<T>> @try) =>
            @try.Match(Ok, ServerError);

        public static Task<IActionResult> ToActionResult<T>(this TryAsync<T> @try) =>
            @try.Match(Ok, ServerError);

        public static Task<IActionResult> ToActionResult<T>(this Try<Task<T>> @try) =>
            @try.ToAsync().ToActionResult();
    }
}
