using Library.Models.DTOs.Cnr;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace WS_DATA.Services
{
    public class CnrService
    {
        private readonly HttpClient _httpClient;
        private readonly CnrSettings _settings;

        public CnrService(HttpClient httpClient, IOptions<CnrSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        }
        public async Task<ConsultaNit?> ConsultarEntidadPorNitAsync(string nit)
        {
            var url = $"comunes/consultaxNit?nit={nit}";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null; 
            }
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorContent = await response.Content.ReadAsStringAsync();
            //    throw new Exception($"Error al consultar por NIT: {response.StatusCode}, detalle: {errorContent}");
            //}
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<ConsultaNit>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista?.FirstOrDefault();
        }
        public async Task<PersonaJuridica?> ConsultarPersonaJuridicaAsync(string codigoComunes)
        {
            var url = $"comercio/personaJuridica?codigoComunes={codigoComunes}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PersonaJuridica>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public async Task<List<CredencialPersonaNatural>> ConsultarCredencialPersonaNaturalAsync(string codigoComun)
        {
            var url = $"comercio/credencialesPersonaNatural?codigoComunes={codigoComun}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<CredencialPersonaNatural>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<PersonaNatural> ConsultarPersonaNaturalAsync(string codigoComun)
        {
            var url = $"comercio/personaNatural?codigoComunes={codigoComun}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<PersonaNatural>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<List<Sucursal>> ConsultarSucursalAsync(string codigoComun)
        {
            var url = $"comercio/sucursales?codigoComunes={codigoComun}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Sucursal>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<List<Participante>> ConsultarParticipanteAsync(string codigoComun)
        {
            var url = $"comercio/participantes?codigoComunes={codigoComun}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Participante>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<List<MatriculaPersona>> ConsultarMatriculaPorPersonaAsync(string codigoComun)
        {
            var url = $"consultas/rprh/consultaMatriculasXPer?codPersona={codigoComun}";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<MatriculaPersona>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<DatosMatricula> ConsultarDatosMatriculaAsync(string matricula)
        {
            var url = $"consultas/rprh/matriculaDatos?matricula={matricula}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<DatosMatricula> (content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<List<Titular>> ConsultarTitularAsync(string matricula)
        {
            var url = $"consultas/rprh/titulares?matricula={matricula}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Titular>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<Inmueble> ConsultarInmuebleAsync(string claveCatastral)
        {
            var url = $"catastro/datosGenerales?claveCatastral={claveCatastral}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Inmueble>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista.FirstOrDefault();
        }
        public async Task<List<Propietario>> ConsultarPropietarioCatastroAsync(string claveCatastral)
        {
            var url = $"catastro/propietariosCatastro?claveCatastral={claveCatastral}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Propietario>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
        public async Task<List<Coordenada>> ConsultarCoordenadaAsync(string claveCatastral)
        {
            var url = $"catastro/coordenadas?claveCatastral={claveCatastral}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Coordenada>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return lista;
        }
    }
}
