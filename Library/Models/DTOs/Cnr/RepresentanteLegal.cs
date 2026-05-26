using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class RepresentanteLegal
    {
        public string NombreCompleto { get; set; }
        public string NumeroInscripcion { get; set; }
        public string LibroInscripcion { get; set; }
        public string FechaInscripcion { get; set; }
        public string NumeroPresentacion { get; set; }
        public string PaisNacionalidad { get; set; }
        public string PaisDomicilio { get; set; }
        public string Estado { get; set; }
        public List<DocumentoIdentidad> Documentos { get; set; }
    }
}
