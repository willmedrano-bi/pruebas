using Library.Models.DTOs.Cnr;
using Library.Models.DTOs.Rnpn;
using Library.Models.Unificada;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using WA_DATA.Helpers;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace WA_DATA.Controllers
{
    [Authorize]
    [Authorize(Roles = "cnr-rol")]
    public class CnrController : BaseController
    {
        private readonly ILogger<CnrController> _logger;
        private readonly IConfiguration _config;
        public CnrController(ILogger<CnrController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarPorNit([FromBody] NitRequest request)
        {
          
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/nit";
                var postBody = new { nit = request.nit };
                var response = await client.PostAsJsonAsync(url, postBody);

                //var responseContent = await response.Content.ReadAsStringAsync();
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<PersonaJuridica>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }

        public IActionResult Entidad()
        {
            return View();
        }
        public IActionResult Matricula()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarCodigoComun([FromBody] NitRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/CodigoComun";
                var postBody = new { nit = request.nit };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<ConsultaNit>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarCodigoComunPartial([FromBody] ConsultaNit data)
        {
            if (data == null)
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_codigoComun", data);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarPersonaJuridica([FromBody] CodigoComunRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/PersonaJuridica";
                var postBody = new { CodigoComun = request.CodigoComun };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<PersonaJuridica>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarPersonaJuridicaPartial([FromBody] PersonaJuridica data)
        {
            if (data == null)
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_personaJuridica", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarCredencialPersonaNatural([FromBody] CodigoComunRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/CredencialPersonaNatural";
                var postBody = new { CodigoComun = request.CodigoComun };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<CredencialPersonaNatural>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarCredencialPersonaNaturalPartial([FromBody] List<CredencialPersonaNatural> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_credencialPersonaNatural", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarPersonaNatural([FromBody] CodigoComunRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/PersonaNatural";
                var postBody = new { CodigoComun = request.CodigoComun };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<PersonaNatural>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarPersonaNaturalPartial([FromBody] PersonaNatural data)
        {
            if (data == null)
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_personaNatural", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarSucursal([FromBody] CodigoComunRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/Sucursal";
                var postBody = new { CodigoComun = request.CodigoComun };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<Sucursal>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarSucursalPartial([FromBody] List<Sucursal> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_sucursal", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarParticipante([FromBody] CodigoComunRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/Participante";
                var postBody = new { CodigoComun = request.CodigoComun };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<Participante>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarParticipantePartial([FromBody] List<Participante> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_participante", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarMatricula([FromBody] CodigoComunRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/MatriculaPersona";
                var postBody = new { CodigoComun = request.CodigoComun };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<MatriculaPersona>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarMatriculaPartial([FromBody] List<MatriculaPersona> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_matricula", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarDatosMatricula([FromBody] MatriculaRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/DatosMatricula";
                var postBody = new { Matricula = request.Matricula };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<DatosMatricula>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarDatosMatriculaPartial([FromBody] DatosMatricula data)
        {
            if (data == null)
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_datosMatricula", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarTitular([FromBody] MatriculaRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/Titular";
                var postBody = new { Matricula = request.Matricula };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<Titular>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarTitularPartial([FromBody] List<Titular> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_titular", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarInmueble([FromBody] ClaveCatastralRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/Inmueble";
                var postBody = new { ClaveCatastral = request.ClaveCatastral };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<Inmueble>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarInmueblePartial([FromBody] Inmueble data)
        {
            if (data == null)
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_inmueble", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarPropietarioCatastro([FromBody] ClaveCatastralRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/PropietarioCatastro";
                var postBody = new { ClaveCatastral = request.ClaveCatastral };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<Propietario>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarPropietarioCatastroPartial([FromBody] List<Propietario> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_propietario", data);
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarCoordenada([FromBody] ClaveCatastralRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Json(new
                {
                    success = false,
                    message = "Token no disponible.",
                    code = 401
                });
            }
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var baseUrl = _config["WS-Service:CnrUrl"];
                var url = $"{baseUrl}/Coordenada";
                var postBody = new { ClaveCatastral = request.ClaveCatastral };
                var response = await client.PostAsJsonAsync(url, postBody);
                var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(CnrResponse<List<Coordenada>>));
                return Json(standardizedResponse);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error interno al procesar la consulta.",
                    code = 500,
                    detail = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult ConsultarCoordenadaPartial([FromBody] List<Coordenada> data)
        {
            if (data == null || !data.Any())
                return BadRequest("Datos vacíos o inválidos");
            return PartialView("_coordenada", data);
        }
    }
}
