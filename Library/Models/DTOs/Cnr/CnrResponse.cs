using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Rnpn
{
    public class CnrResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
