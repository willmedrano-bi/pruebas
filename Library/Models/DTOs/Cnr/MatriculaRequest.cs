using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class MatriculaRequest
    {
        [Required(ErrorMessage = "La Matricula es obligatoria.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La Matricula debe de contener solo números.")]
        public string Matricula { get; set; } = string.Empty;
    }
}
