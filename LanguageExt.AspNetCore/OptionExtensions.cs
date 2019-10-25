using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LanguageExt.AspNetCore.Common;

namespace LanguageExt.AspNetCore
{
    public static class OptionExtensions
    {
        public static IActionResult ToActionResult<T>(this Option<T> option) =>
            option.Match(Ok, NotFound);

        public static Task<IActionResult> ToActionResult<T>(this Task<Option<T>> option) =>
            option.Map(ToActionResult);

        public static Option<JsonResult> ToJsonResult<T>(this Option<T> option) =>
            option.Map(OkJson);

        public static Task<Option<JsonResult>> ToJsonResult<T>(this Task<Option<T>> option) =>
            option.Map(ToJsonResult);

        public static Task<IActionResult> ToActionResult<T>(this OptionAsync<T> option) =>
            option.Match(Ok, NotFound);
    }
}
