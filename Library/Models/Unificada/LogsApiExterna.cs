using System;
using System.Collections.Generic;

namespace Library.Models.Unificada;

public partial class LogsApiExterna
{
    public int IdLogApiExterna { get; set; }

    public DateTime FechaConsulta { get; set; }

    public Guid UserId { get; set; }

    public string? PayloadSolicitud { get; set; }

    public bool Exito { get; set; }

    public string? OrigenIp { get; set; }

    public string? SistemaOrigen { get; set; }

    public string? PayloadRespuesta { get; set; }
}
