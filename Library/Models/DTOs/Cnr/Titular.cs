using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class Titular
    {
        public string CodigoComunes { get; set; }
        public string NombreTitular { get; set; }
        public int CorrelativoNombre { get; set; }
        public string Derecho { get; set; }
        public int Asiento { get; set; }
        public double PorcentajeDerecho { get; set; }
        public string Desde { get; set; }
    }
}
