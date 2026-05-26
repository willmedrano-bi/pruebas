using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using WA_DATA.Helpers;
using WA_DATA.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.Cookie.SameSite = SameSiteMode.Unspecified;
                                                                
});

builder
    .Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddKeycloakWebApp(
        builder.Configuration.GetSection(KeycloakAuthenticationOptions.Section),
        configureOpenIdConnectOptions: options =>
        {
            // we need this for front-channel sign-out
            options.SaveTokens = true;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.None;

            options.NonceCookie.SameSite = SameSiteMode.Unspecified;
            options.NonceCookie.SecurePolicy = CookieSecurePolicy.None;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RoleClaimType = ClaimTypes.Role
            };
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.Events = new OpenIdConnectEvents
            {
                OnSignedOutCallbackRedirect = context =>
                {
                    context.Response.Redirect("/Home/index");
                    context.HandleResponse();

                    return Task.CompletedTask;
                }, OnTokenValidated = context =>
                {
                    var identity = (ClaimsIdentity)context.Principal.Identity;

                    var accessToken = context.TokenEndpointResponse.AccessToken;
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(accessToken);

                    // Serializa payload a JSON string
                    var payloadJson = System.Text.Json.JsonSerializer.Serialize(jwt.Payload);
                    using var doc = JsonDocument.Parse(payloadJson);

                    var root = doc.RootElement;

                    if (root.TryGetProperty("resource_access", out var resourceAccess) &&
                        resourceAccess.TryGetProperty("identidad_client", out var identidadClient) &&
                        identidadClient.TryGetProperty("roles", out var rolesElement) &&
                        rolesElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var role in rolesElement.EnumerateArray())
                        {
                            var roleValue = role.GetString();
                            if (!string.IsNullOrEmpty(roleValue))
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                            }
                        }
                    }
                    return Task.CompletedTask;
                }
            };
        }
    );

builder
    .Services.AddKeycloakAuthorization(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("rnpn", policy => policy.RequireResourceRolesForClient("identidad_client", ["rnpn-rol"]))
    .AddPolicy("cnr", policy => policy.RequireResourceRolesForClient("identidad_client", ["cnr-rol"]));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TokenRefreshHelper>();

var app = builder.Build();




// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();



//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<TokenRefreshMiddleware>();
app.UseAuthorization();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
