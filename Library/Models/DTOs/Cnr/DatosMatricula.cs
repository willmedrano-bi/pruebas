using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class DatosMatricula
    {
        public string EstadoFolio { get; set; }
        public double Area { get; set; }
        public double RestoRegistral { get; set; }
        public int Volumen { get; set; }
        public string NaturalezaInmueble { get; set; }
        public string FechaCreacionMatricula { get; set; }
        public string? AntecedenteFolioReal { get; set; }
        public string? AntecedenteLibro { get; set; }
        public int? NumeroLibro { get; set; }
        public int? NumeroInscripcion { get; set; }
        public string Direccion { get; set; }
        public string? NombreInmueble { get; set; }
        public string ClaveCatastral { get; set; }
    }
}
