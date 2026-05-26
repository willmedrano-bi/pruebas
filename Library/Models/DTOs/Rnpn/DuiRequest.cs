using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Rnpn
{
    public class DuiRequest
    {
        [Required(ErrorMessage = "El DUI es requerido")]
        [RegularExpression(@"^\d{8}-\d$", ErrorMessage = "El DUI debe tener el formato 12345678-9")]
        [Display(Name = "Documento Único de Identidad")]
        public string Dui { get; set; }
    }
}
