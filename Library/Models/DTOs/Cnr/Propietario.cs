using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class Propietario
    {
        public string NombrePropietario { get; set; }
        public string TipoPropietario { get; set; }

        public string? Dui { get; set; }
        public string? Nit { get; set; }
        public string? Cip { get; set; }
        public string? Pasaporte { get; set; }
        public string? CedulaVecindad { get; set; }
        public string? CarnetResidente { get; set; }
    }
}
