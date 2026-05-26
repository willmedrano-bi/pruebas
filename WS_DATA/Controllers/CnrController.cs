using Library.Models.DTOs.Cnr;
using Library.Models.Unificada;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using WS_DATA.Services;

namespace WS_DATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "cnr-rol")]
    public class CnrController : ControllerBase
    {
        private readonly IAuditoriaService _auditoria;
        private readonly CnrService _service;

        public CnrController(CnrService service, IAuditoriaService auditoria)
        {
            _service = service;
            _auditoria = auditoria;
        }
        [HttpPost("nit")]
        public async Task<IActionResult> ConsultarPorNit([FromBody] NitRequest request)
        {
            ConsultaNit? _consultaNit = null;
            PersonaJuridica? result = null;
            var exito = true;
            try
            {
                _consultaNit = await  _service.ConsultarEntidadPorNitAsync(request.nit);
                if (_consultaNit == null || string.IsNullOrWhiteSpace(_consultaNit.CodigoComunes))
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                result = await _service.ConsultarPersonaJuridicaAsync(_consultaNit.CodigoComunes);

                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "nit", value = request.nit }
                };

                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("CodigoComun")]
        public async Task<IActionResult> ConsultarPorCodigoComun([FromBody] NitRequest request)
        {
            ConsultaNit? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarEntidadPorNitAsync(request.nit);
                if (result == null || string.IsNullOrWhiteSpace(result.CodigoComunes))
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "nit", value = request.nit }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("PersonaJuridica")]
        public async Task<IActionResult> ConsultarPersonaJuridica([FromBody] CodigoComunRequest request)
        {
            PersonaJuridica? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarPersonaJuridicaAsync(request.CodigoComun);
                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "codigoComun", value = request.CodigoComun }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("CredencialPersonaNatural")]
        public async Task<IActionResult> ConsultarCredencialPersonaNatural([FromBody] CodigoComunRequest request)
        {
            List<CredencialPersonaNatural>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarCredencialPersonaNaturalAsync("55318478");
                if (result == null || !result.Any())
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "codigoComun", value = request.CodigoComun }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("PersonaNatural")]
        public async Task<IActionResult> ConsultarPersonaNatural([FromBody] CodigoComunRequest request)
        {
            PersonaNatural? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarPersonaNaturalAsync(request.CodigoComun);
                if (result == null )
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "codigoComun", value = request.CodigoComun }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("Sucursal")]
        public async Task<IActionResult> ConsultarSucursal([FromBody] CodigoComunRequest request)
        {
            List<Sucursal>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarSucursalAsync(request.CodigoComun);
                if (result == null || !result.Any())
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "codigoComun", value = request.CodigoComun }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("Participante")]
        public async Task<IActionResult> ConsultarParticipante([FromBody] CodigoComunRequest request)
        {
            List<Participante>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarParticipanteAsync(request.CodigoComun);
                if (result == null || !result.Any())
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "codigoComun", value = request.CodigoComun }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("MatriculaPersona")]
        public async Task<IActionResult> ConsultarMatriculaPorPersona([FromBody] CodigoComunRequest request)
        {
            List<MatriculaPersona>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarMatriculaPorPersonaAsync(request.CodigoComun);
                if (result == null || !result.Any())
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "codigoComun", value = request.CodigoComun }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("DatosMatricula")]
        public async Task<IActionResult> ConsultarDatosMatricula([FromBody] MatriculaRequest request)
        {
            DatosMatricula? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarDatosMatriculaAsync(request.Matricula);
                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad con ese NIT.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "matricula", value = request.Matricula }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("Titular")]
        public async Task<IActionResult> ConsultarTitular([FromBody] MatriculaRequest request)
        {
            List<Titular>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarTitularAsync(request.Matricula);
                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "matricula", value = request.Matricula }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("Inmueble")]
        public async Task<IActionResult> ConsultarInmueble([FromBody] ClaveCatastralRequest request)
        {
            Inmueble? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarInmuebleAsync(request.ClaveCatastral);
                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "matricula", value = request.ClaveCatastral }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("PropietarioCatastro")]
        public async Task<IActionResult> ConsultarPropietarioCatastro([FromBody] ClaveCatastralRequest request)
        {
            List<Propietario>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarPropietarioCatastroAsync(request.ClaveCatastral);
                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "matricula", value = request.ClaveCatastral }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }
        [HttpPost("Coordenada")]
        public async Task<IActionResult> ConsultarCoordenadaCatastro([FromBody] ClaveCatastralRequest request)
        {
            List<Coordenada>? result = null;
            var exito = true;
            try
            {
                result = await _service.ConsultarCoordenadaAsync(request.ClaveCatastral);
                if (result == null)
                {
                    exito = false;
                    return NotFound(new
                    {
                        Success = false,
                        Message = "No se encontró ninguna entidad.",
                        code = 404
                    });
                }
                return Ok(new
                {
                    Success = true,
                    Message = "Consulta exitosa",
                    code = 200,
                    data = result
                });
            }
            catch (Exception ex)
            {
                exito = false;
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error interno al consultar el CNR.",
                    code = 500,
                    detail = ex.Message
                });
                throw;
            }
            finally
            {
                var filtros = new List<object>
                {
                    new { name = "matricula", value = request.ClaveCatastral }
                };
                var payload = result is not null ? System.Text.Json.JsonSerializer.Serialize(result) : null;
                await _auditoria.RegistrarAsync(new LogsApiExterna
                {
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    PayloadSolicitud = JsonConvert.SerializeObject(filtros),
                    PayloadRespuesta = payload,
                    Exito = exito,
                    OrigenIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    SistemaOrigen = "API-CNR"
                });
            }
        }


    }
}
