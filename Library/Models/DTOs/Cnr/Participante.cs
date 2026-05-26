using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class Participante
    {
        public string TipoPresentacion { get; set; }
        public string Presentacion { get; set; }
        public string Funcion { get; set; }
        public string TipoPersona { get; set; }
        public string CodigoPersona { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string FechaInscripcion { get; set; } 
        public string NumeroInscripcion { get; set; }
        public string NumeroLibro { get; set; }
    }
}
