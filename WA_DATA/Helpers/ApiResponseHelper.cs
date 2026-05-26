using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace WA_DATA.Helpers
{
    public static class ApiResponseHelper
    {
        public static async Task<object> ProcessApiResponse(HttpResponseMessage response, Type tipoRespuesta)
        {
            var statusCode = (int)response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();
            string message = null;
            if (string.IsNullOrWhiteSpace(content))
            {
                return new
                {
                    success = false,
                    message = $"Error {statusCode}: respuesta vacía del servidor.",
                    code = statusCode
                };
            }

            // Intentar deserializar el JSON
            dynamic json;
            try
            {
                json = JsonConvert.DeserializeObject<dynamic>(content);
            }
            catch
            {
                return new
                {
                    success = false,
                    message = "No se pudo interpretar la respuesta del servidor.",
                    code = statusCode,
                    raw = content
                };
            }

            // 200–299: éxito
           /* if (response.IsSuccessStatusCode)
            {
                return new
                {
                    success = true,
                    message = json?.message ?? "Consulta exitosa",
                    code = statusCode,
                    data = json?.data
                };
            }*/
            if (response.IsSuccessStatusCode)
            {
                var objeto = JsonConvert.DeserializeObject(content, tipoRespuesta);
                var dataProperty = tipoRespuesta.GetProperty("Data");
                var data = dataProperty?.GetValue(objeto);

                return new
                {
                    success = true,
                    message = (String)json.message ?? "Consulta exitosa",
                    code = statusCode,
                    data = data
                };
            }


            // 400: error de validación tipo ProblemDetails
            if (statusCode == 400 && json?.errors != null)
            {
                var firstError = ((IDictionary<string, JToken>)json.errors).First().Value.First?.ToString();
                return new
                {
                    success = false,
                    message = firstError ?? "Error de validación.",
                    code = statusCode,
                    detail = json?.title
                };
            }

            //  404: recurso no encontrado
            if (statusCode == 404)
            {
                return new
                {
                    success = false,
                    message = (String)json.message ??  "No se encontró el recurso solicitado.",
                    code = statusCode
                };
            }

            //  401: no autorizado
            if (statusCode == 401)
            {
                return new
                {
                    success = false,
                    message = "No autorizado. Verifique su sesión o token.",
                    code = statusCode
                };
            }

            //  429: demasiadas solicitudes
            if (statusCode == 429)
            {
                return new
                {
                    success = false,
                    message = (String)json?.Message ?? (String)json?.message ?? "Demasiadas solicitudes. Intente más tarde.",
                    code = statusCode
                };
            }

            //  500+: error del servidor
            if (statusCode >= 500)
            {
                return new
                {
                    success = false,
                    message = (String)json?.Message ?? (String)json?.message ?? "Error interno del servidor.",
                    code = statusCode,
                    detail = json?.detail ?? json
                };
            }

            //  Otros casos no controlados
            return new
            {
                success = false,
                message = (String)json?.Message ?? (String)json?.message ?? "Error desconocido.",
                code = statusCode,
                detail = json
            };
        }
    }
}