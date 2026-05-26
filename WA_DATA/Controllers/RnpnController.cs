using Library.Models.DTOs.Cnr;
using Library.Models.DTOs.Rnpn;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using WA_DATA.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WA_DATA.Controllers
{
    [Authorize]
    [Authorize(Roles = "rnpn-rol")]
    public class RnpnController : BaseController
    {
        private readonly ILogger<RnpnController> _logger;
        private readonly IConfiguration _config;
        public RnpnController(ILogger<RnpnController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConsultarPorDuiPartial([FromBody] RnpnData data)
        {
            if (data == null)
                return BadRequest("Datos vacíos o inválidos");

            return PartialView("_RnpnResultado", data); 
        }
        public IActionResult Historico()
        {
            return View();
        }
        public IActionResult Nombre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConsultarDui([FromBody] DuiRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
                return StatusCode(401, new { success = false, Message = "Token no disponible." });
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var baseUrl = _config["WS-Service:RnpnUrl"];
                    var url = $"{baseUrl}/dui";
                    var postBody = new { Dui = request.Dui };
                    var response = await client.PostAsJsonAsync(url, postBody);
                    //var responseContent = await response.Content.ReadAsStringAsync();


                    var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(RnpnResponse<RnpnData>));
                    return Json(standardizedResponse);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var result = JsonConvert.DeserializeObject<RnpnResponse<RnpnData>>(responseContent);
                    //    return Ok(result.Data.FirstOrDefault());
                    //}
                    //else
                    //{
                    //    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<dynamic>(responseContent));
                    //}
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, Message = $"Excepción: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarHistorico([FromBody] DuiRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
                return StatusCode(401, new { success = false, message = "Token no disponible." });
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var url = _config["WS-Service:RnpnUrl"] + "/historico"; 
                    var response = await client.PostAsJsonAsync(url, request);
                   // var responseContent = await response.Content.ReadAsStringAsync();

                    var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(RnpnResponse<RnpnPersona>));
                    return Json(standardizedResponse);
                    //if (response.IsSuccessStatusCode){
                    //    var result = JsonConvert.DeserializeObject<RnpnResponse<RnpnPersona>>(responseContent);
                    //    return Ok(result.Data);
                    //}
                    //else{
                    //    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<dynamic>(responseContent));
                    //}
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Excepción: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarNombre([FromBody] PersonaRequest request)
        {
            
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
                return StatusCode(401, new { success = false, message = "Token no disponible." });
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var url = _config["WS-Service:RnpnUrl"] +"/nombre"; 
                    var response = await client.PostAsJsonAsync(url, request);
                    //var responseContent = await response.Content.ReadAsStringAsync();
                    var standardizedResponse = await ApiResponseHelper.ProcessApiResponse(response, typeof(RnpnResponse<RnpnData>));
                    return Json(standardizedResponse);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var result = JsonConvert.DeserializeObject<RnpnResponse<RnpnData>>(responseContent);
                    //    return Ok(result);
                    //}
                    //else
                    //{
                    //    return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject<dynamic>(responseContent));
                    //}
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Excepción: " + ex.Message });
            }
        }
    }
}
