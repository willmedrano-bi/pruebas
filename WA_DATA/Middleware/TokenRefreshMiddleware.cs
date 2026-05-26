using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WA_DATA.Helpers;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Text.Json;
namespace WA_DATA.Middleware
{
    public class TokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenRefreshMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TokenRefreshHelper tokenHelper)
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                var isAjax = context.Request.Headers["Content-type"].ToString().Contains("application/json");

                if (isAjax)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //await context.Response.WriteAsync("No autorizado");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = "Sesión expirada. Por favor, inicie sesión nuevamente."
                    }));
                    return;
                }

                // Dejar que la redirección normal ocurra para peticiones no-AJAX
                await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme);
                return;
            }
            var expiresAt = await context.GetTokenAsync("expires_at");
            if (DateTime.TryParse(expiresAt, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var expiration))
            {
                if (expiration < DateTime.UtcNow)
                {
                    var refreshToken = await context.GetTokenAsync("refresh_token");

                    // Llama al endpoint de refresh token (a Keycloak)
                    var newTokens = await tokenHelper.PedirNuevoAccessTokenAsync(refreshToken);

                    if (newTokens != null)
                    {
                        var authInfo = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                        authInfo.Properties.UpdateTokenValue("access_token", newTokens.AccessToken);
                        authInfo.Properties.UpdateTokenValue("refresh_token", newTokens.RefreshToken);
                        authInfo.Properties.UpdateTokenValue("expires_at", DateTime.UtcNow.AddSeconds(newTokens.ExpiresIn).ToString("o"));

                        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authInfo.Principal, authInfo.Properties);
                    }
                    if (newTokens == null || string.IsNullOrEmpty(newTokens.AccessToken))
                    {
                        var isAjax = context.Request.Headers["Content-type"].ToString().Contains("application/json");

                        if (isAjax)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            //await context.Response.WriteAsync("No autorizado");
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                success = false,
                                message = "Sesión expirada. Por favor, inicie sesión nuevamente."
                            }));
                        }
                        else
                        {
                            await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme);
                        }
                        return;
                    }
                }
            }
        await _next(context);
        }
    }
}
