using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class BalanceDepositado
    {
        public string NumeroPresentacion { get; set; }
        public string TipoBalance { get; set; }
        public string Anio { get; set; }
        public string FechaDeposito { get; set; }
        public string NumeroDeposito { get; set; }
        public string Activo { get; set; }
    }
}
