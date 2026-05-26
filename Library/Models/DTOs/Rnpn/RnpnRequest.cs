using System.Collections.Generic;

namespace Library.Models.DTOs.Rnpn
{
    public class RnpnRequest
    {
        public string CodiApli { get; set; }
        public string NombUsua { get; set; }
        public string DireIP { get; set; }
        public string Tokn { get; set; }
        public string NumeTram { get; set; }
        public string NombFunc { get; set; }
        public List<Filtro> Filt { get; set; }
    }
}
