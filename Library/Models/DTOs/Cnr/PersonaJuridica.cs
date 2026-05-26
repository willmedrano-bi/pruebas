using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class PersonaJuridica
    {
        public string Denominacion { get; set; }
        public string Abreviatura { get; set; }
        public string CodigoPersona { get; set; }
        public string EstadoPersona { get; set; }
        public string FechaInscripcion { get; set; }
        public string NumeroInscripcion { get; set; }
        public string LibroInscripcion { get; set; }
        public string CapitalInscrito { get; set; }
        public string Nit { get; set; }
        public DatosEmpresa DatosEmpresa { get; set; }
        public List<BalanceDepositado> BalancesDepositados { get; set; }
        public List<RepresentanteLegal> RepresentantesLegales { get; set; } // ← corregido
        public List<Apoderado> Apoderados { get; set; }
    }
}
