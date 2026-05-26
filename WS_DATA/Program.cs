using Library.Models.DTOs.Rnpn;
using Library.Models.Unificada;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WS_DATA.Services;

using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Library.Models.DTOs.Cnr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = $"{builder.Configuration["Keycloak:auth-server-url"]}realms/{builder.Configuration["Keycloak:realm"]}";
        options.Audience = "account";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = $"{builder.Configuration["Keycloak:auth-server-url"]}realms/{builder.Configuration["Keycloak:realm"]}",
            RoleClaimType = "roles",
            ValidateLifetime = true
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;
                if (resourceAccess != null)
                {
                    var parsed = System.Text.Json.JsonDocument.Parse(resourceAccess);
                    if (parsed.RootElement.TryGetProperty(builder.Configuration["Keycloak:resource"], out var identidadClient) &&
                        identidadClient.TryGetProperty("roles", out var roles))
                    {
                        foreach (var role in roles.EnumerateArray())
                        {
                            claimsIdentity.AddClaim(new Claim("roles", role.GetString()));
                        }
                    }
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WS-RNPN", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT con el prefijo **Bearer**"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.Configure<RnpnSettings>(builder.Configuration.GetSection("RnpnSettings")); // carga de modelado de datos
// Add services to the container.
builder.Services.AddHttpClient<RnpnService>();
builder.Services.Configure<CnrSettings>(builder.Configuration.GetSection("CnrSettings"));
builder.Services.AddHttpClient<CnrService>();

builder.Services.AddDbContext<UnificadaSpkeyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
//configuración del limite de peticiones
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        // Identifica por usuario autenticado o IP
        var userId = httpContext.User?.FindFirst("sub")?.Value
                     ?? httpContext.Connection.RemoteIpAddress?.ToString()
                     ?? "anon";

        return RateLimitPartition.GetFixedWindowLimiter(userId, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10, // máx. 10 peticiones
            Window = TimeSpan.FromMinutes(1), // por minuto
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });
    });
    options.RejectionStatusCode = 429;
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        context.HttpContext.Response.ContentType = "application/json";

        var respuesta = new { success = false, Message = "Demasiadas solicitudes. Intente nuevamente más tarde." };

        var json = System.Text.Json.JsonSerializer.Serialize(respuesta);

        await context.HttpContext.Response.WriteAsync(json, token);
    };
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WS-RNPN v1");
        c.RoutePrefix = "swagger";
    });
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();


app.MapControllers();

app.Run();
