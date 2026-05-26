using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class DocumentoIdentidad
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string PaisEmision { get; set; }
    }
}
