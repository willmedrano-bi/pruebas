using Library.Models.DTOs.Token;

namespace WA_DATA.Helpers
{
    public class TokenRefreshHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenRefreshHelper> _logger;

        public TokenRefreshHelper(IConfiguration configuration, ILogger<TokenRefreshHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<TokenResponse?> PedirNuevoAccessTokenAsync(string refreshToken)
        {
            var client = new HttpClient();

            var keycloakSection = _configuration.GetSection("Keycloak");
            var tokenEndpoint = keycloakSection["auth-server-url"] + "realms/"+ keycloakSection["realm"] + "/protocol/openid-connect/token";
            var clientId = keycloakSection["resource"];
            var clientSecret = keycloakSection.GetSection("credentials")["secret"];

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        });

            try
            {
                var response = await client.PostAsync(tokenEndpoint, content);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Refresh token request failed: {StatusCode}", response.StatusCode);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var token = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(json,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while requesting new access token from Keycloak");
                return null;
            }
        }
    }
}
