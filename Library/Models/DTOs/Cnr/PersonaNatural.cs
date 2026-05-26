using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class PersonaNatural
    {
        public string Nombre { get; set; }
        public string NumeroInscripcion { get; set; }
        public string UltimaRenovacion { get; set; }
        public string DireccionMatriz { get; set; }
        public string UltimoBalance { get; set; }
        public string ActivoUltimoBalance { get; set; }
    }
}
