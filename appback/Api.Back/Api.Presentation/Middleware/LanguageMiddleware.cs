using Api.Common.Utils;
using Microsoft.Extensions.Primitives;

namespace Api.Presentation.Middleware
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly LanguageSettings _languageSettings;

        public LanguageMiddleware(RequestDelegate next, LanguageSettings languageSettings)
        {
            _next = next;
            _languageSettings = languageSettings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("language", out StringValues languages))
            {
                var language = languages.ToString().ToLower();
                switch (language)
                {
                    case "es":
                        _languageSettings.Language = "es_ES";
                        break;

                    case "en":
                        _languageSettings.Language = "en_US";
                        break;

                    default:
                        _languageSettings.Language = "es_ES";
                        break;
                }
            }
            else
            {
                _languageSettings.Language = "es_ES";
            }
            await _next(context);
        }
    }

    public static class LanguageMiddlewareExtensions
    {
        public static IApplicationBuilder UseLanguageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LanguageMiddleware>();
        }
    }
}