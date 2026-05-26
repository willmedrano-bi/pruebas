using System.Collections.Generic;

namespace Library.Models.DTOs.Rnpn
{
    public class RnpnResponse<T>
    {
        public bool Resp { get; set; }
        public List<T> Data { get; set; }
    }
}
