using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class CodigoComunRequest
    {
        [Required(ErrorMessage = "El código de comunes es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código de comunes no debe exceder los 10 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El código de comunes debe contener solo números.")]
        public string CodigoComun { get; set; } = string.Empty;
    }
}
