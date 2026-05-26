using Library.Models.DTOs.Rnpn;
using Microsoft.Extensions.Options;
namespace WS_DATA.Services   
{
    public class RnpnService
    {
        private readonly HttpClient _httpClient;
        private readonly RnpnSettings _settings;

        public RnpnService(HttpClient httpClient, IOptions<RnpnSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }
        public async Task<RnpnResponse<T>> ConsultarAsync<T>(string nombFunc, string numeTram, List<Filtro> filtros)
        {
            var request = new RnpnRequest
            {
                CodiApli = _settings.CodiApli,
                NombUsua = _settings.NombUsua,
                DireIP = _settings.DireIP,
                Tokn = _settings.Tokn,
                NumeTram = numeTram,
                NombFunc = nombFunc,
                Filt = filtros
            };

            var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl, request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RnpnResponse<T>>();
        }
        public async Task<RnpnResponse<T>> ConsultarHistoricoAsync<T>(string nombFunc, string numeTram, List<Filtro> filtros)
        {
            var request = new RnpnRequest
            {
                CodiApli = _settings.CodiApli,
                NombUsua = _settings.NombUsua,
                DireIP = _settings.DireIP,
                Tokn = _settings.Tokn,
                NumeTram = numeTram,
                NombFunc = nombFunc,
                Filt = filtros
            };

            var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl, request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RnpnResponse<T>>();
           
        }
        public async Task<RnpnResponse<T>> ConsultarNombreAsync<T>(string nombFunc, string numeTram, List<Filtro> filtros)
        {
            var request = new RnpnRequest
            {
                CodiApli = _settings.CodiApli,
                NombUsua = _settings.NombUsua,
                DireIP = _settings.DireIP,
                Tokn = _settings.Tokn,
                NumeTram = numeTram,
                NombFunc = nombFunc,
                Filt = filtros
            };

            var response = await _httpClient.PostAsJsonAsync(_settings.BaseUrl, request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RnpnResponse<T>>();

        }
    }
}
