using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Rnpn
{
    public class PersonaRequest
    {
        [Display(Name = "Primer nombre")]
        public string? Nom1
        {
            get => _nom1;
            set => _nom1 = value?.ToUpperInvariant();
        }
        private string? _nom1;

        [Display(Name = "Segundo nombre")]
        public string? Nom2
        {
            get => _nom2;
            set => _nom2 = value?.ToUpperInvariant();
        }
        private string? _nom2;

        [Display(Name = "Primer apellido")]
        public string? Ape1
        {
            get => _ape1;
            set => _ape1 = value?.ToUpperInvariant();
        }
        private string? _ape1;

        [Display(Name = "Segundo apellido")]
        public string? Ape2
        {
            get => _ape2;
            set => _ape2 = value?.ToUpperInvariant();
        }
        private string? _ape2;

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechNaci { get; set; }
    }
}