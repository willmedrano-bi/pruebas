using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class NitRequest
    {
        [Display(Name = "Número de Identificación Tributaria")]
        [RegularExpression(@"^\d{4}-\d{6}-\d{3}-\d{1}$", ErrorMessage = "El NIT debe tener el formato 0614-100915-110-3")]
        [Required(ErrorMessage = "El NIT es obligatorio.")]
        public string nit { get; set; }

    }
}
