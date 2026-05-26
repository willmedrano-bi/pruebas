using System;
using System.Collections.Generic;

namespace Library.Models.RRHH;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? CodigoEmpleado { get; set; }

    public string? FotoEmpleado { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? PrefijoAcademico { get; set; }

    public string? CorreoPersonal { get; set; }

    public string? DireccionEmpleado { get; set; }

    public string? TelefonoPersonal { get; set; }

    public string? CelularPersonal { get; set; }

    public int? IdGenero { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? CodigoPais { get; set; }

    public int? IdDepartamentoNacimiento { get; set; }

    public int? IdMunicipioNacimiento { get; set; }

    public int? IdCiudadNacimiento { get; set; }

    public string? Nacionalidad { get; set; }

    public double? Peso { get; set; }

    public double? Altura { get; set; }

    public int? IdTiposangre { get; set; }

    public int? IdDiscapacidad { get; set; }

    public int? IdEstadocivil { get; set; }

    public int? IdTipocontrato { get; set; }

    public int? IdTipocontratacion { get; set; }

    public string? Responsable { get; set; }

    public string? TelefonoResponsable { get; set; }

    public string? DireccionResponsable { get; set; }

    public string? CorreoInstitucional { get; set; }

    public string? TelefonoInstitucional { get; set; }

    public string? CelularInstitucional { get; set; }

    public string? Salario { get; set; }

    public string? NombreConyuge { get; set; }

    public string? TelefonoConyuge { get; set; }

    public int? IdEstadoUsuario { get; set; }

    public int? Titular { get; set; }

    public DateTime? FechaContratacion { get; set; }

    public int? IdDetEstadoUsuario { get; set; }

    public DateTime? FechaRenuncia { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdProfesion { get; set; }

    public int? IdMarcacion { get; set; }
}
