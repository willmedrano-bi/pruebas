using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class Inmueble
    {
        public string EstadoParcela { get; set; }
        public double Area { get; set; }
        public double Perimetro { get; set; }
        public string Uso { get; set; }
        public string Direccion { get; set; }
        public string TipoParcela { get; set; }
    }
}
