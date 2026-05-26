using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class DatosEmpresa
    {
        public string Matricula { get; set; }
        public string DireccionMatriz { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Distrito { get; set; }
        public string EstadoRenovacionMatricula { get; set; }
        public string ActividadEconomica { get; set; }
        public string Naturaleza { get; set; }
    }
}
