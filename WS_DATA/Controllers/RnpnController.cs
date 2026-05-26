using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Models.DTOs.Rnpn;
using WS_DATA.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using Library.Models.Unificada;
using System.Text.Json;
using System.Security.Claims;
using Library.Models.DTOs.Response;

namespace WS_DATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "rnpn-rol")]
    public class RnpnController : ControllerBase
    {
        private readonly IAuditoriaService _auditoria;
        private readonly RnpnService _service;

        public RnpnController(RnpnService service, IAuditoriaService auditoria)
        {
            _service = service;
            _auditoria = auditoria;
        }

        [HttpPost("dui")]
        public async Task<IActionResult> ConsultarPorDui([FromBody] DuiRequest request)
        {
            //ViewB
            var filtros = new List<Filtro>
            {
                new Filtro { Name = "dui", Value = request.Dui }
            };
            var exito = true;
            RnpnResponse<JsonElement>? result = null;
            try
            {
                result = await _service.ConsultarAsync<JsonElement>("CONS_DOCU_ASA", Guid.NewGuid().ToString(), filtros);
                if (result == null || result.Data == null || !result.Data.Any())
                {
                    exito = false;
                    return NotFound(new 
                    {
                        Success = false,
                        Message = "No se encontró información para el DUI solicitado.",
                        code = 404
                    });
                }
                var items = result.Data;
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = items
                });
            }
            catch(Exception ex)
            {
                exito = false;
                return StatusCode(500, new 
                {
                    Success = false,
                    Message = "Error interno al consultar el RNPN.",
                    code = 500,
                    detail = ex.Message
                });
            }
            finally
            {
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-Rnpn"
                });
            }
        }
        [HttpPost("nombre")]
        public async Task<IActionResult> ConsultarNombre([FromBody] PersonaRequest request)
        {
            if (request == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Message = "No se encontraron parametros de busqueda.",
                    code = 400
                });
            }
            var filtros = new List<Filtro>();

            if (!string.IsNullOrWhiteSpace(request.Nom1))
                filtros.Add(new Filtro { Name = "nom1", Value = request.Nom1 });

            if (!string.IsNullOrWhiteSpace(request.Nom2))
                filtros.Add(new Filtro { Name = "nom2", Value = request.Nom2 });

            if (!string.IsNullOrWhiteSpace(request.Ape1))
                filtros.Add(new Filtro { Name = "ape1", Value = request.Ape1 });

            if (!string.IsNullOrWhiteSpace(request.Ape2))
                filtros.Add(new Filtro { Name = "ape2", Value = request.Ape2 });

            if (request.FechNaci.HasValue)
                filtros.Add(new Filtro
                {
                    Name = "fechNaci",
                    Value = request.FechNaci.Value.ToString("dd/MM/yyyy")
                });
            if (filtros.Count < 3) {
                return NotFound(new
                {
                    Success = false,
                    Message = "Debe proporcionar al menos tres filtros válidos.",
                    code = 400
                });
            }
            var exito = true;
            RnpnResponse<JsonElement>? result = null;
            try{

                result = await _service.ConsultarAsync<JsonElement>("CONS_DOCU_PRMS_ASA", Guid.NewGuid().ToString(), filtros);
                if (result == null || result.Data == null || !result.Data.Any())
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró información.",
                        code = 404
                    });
                }
                var items = result.Data;
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = items
                });
            }
            catch(Exception ex){
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el RNPN.",
                    code = 500,
                    detail = ex.Message
                });
            }
            finally{
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-Rnpn"
                });
            }
        }


        [HttpPost("historico")]
        public async Task<IActionResult> ConsultarHistorico([FromBody] DuiRequest request)
        {
            var filtros = new List<Filtro>
            {
                new Filtro { Name = "dui", Value = request.Dui }
            };
            var exito = true;
            RnpnResponse<JsonElement>? result = null;
            try{
                result = await _service.ConsultarAsync<JsonElement>("CONS_HISTORICO_ASA", Guid.NewGuid().ToString(), filtros);
                if (result == null || result.Data == null || !result.Data.Any())
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró información para el DUI solicitado.",
                        code = 404
                    });
                }
                var items = result.Data;
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = items
                });
            }
            catch(Exception ex){
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el RNPN.",
                    code = 500,
                    detail = ex.Message
                });
            }
            finally{
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-Rnpn"
                });
            }
        }
    }
}
