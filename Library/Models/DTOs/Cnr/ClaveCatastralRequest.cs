using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Cnr
{
    public class ClaveCatastralRequest
    {
        [Required(ErrorMessage = "La clave catastral es obligatoria.")]
        public string ClaveCatastral { get; set; }
    }
}
