using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class CredencialPersonaNatural
    {
        public string Sociedad { get; set; }
        public string Nombramiento { get; set; }
        public DateTime FechaOtorgamiento { get; set; }
        public string TipoDocumento { get; set; }
        public string CodigoServicio { get; set; }
        public string NumeroPresentacion { get; set; }
        public string CodigoSociedad { get; set; }
        public string InsAsiento { get; set; }
        public string NumLibro { get; set; }
        public DateTime InsFecha { get; set; }
        public string TLibro { get; set; }
    }
}
