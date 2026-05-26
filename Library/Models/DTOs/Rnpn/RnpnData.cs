using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTOs.Rnpn
{
    public class RnpnData
    {
        [Display(Name = "Documento unico de identidad")]
        public string dui { get; set; }
        public string nom1 { get; set; }
        public string nom2 { get; set; }
        public string nom3 { get; set; } = "";

        public string ape1 { get; set; }
        public string ape2 { get; set; }
        public string apdoCsda { get; set; } = "";


        public string fechNaci { get; set; }
        public string nombPaisNaci { get; set; }
        public string nombDeptNaci { get; set; }
        public string rMuniNaci { get; set; }
        public string nombMuniNaci { get; set; }


        public string paisExpe { get; set; }
        public string deptExpe { get; set; }
        public string rMuniEmisDui { get; set; }
        public string muniExpe { get; set; }
        public string fechVenc { get; set; }
        public string fechExpe { get; set; }
        

        public string paisDomi { get; set; }
        public string deptDomi { get; set; }
        public string rMuniDomic { get; set; }
        public string municDomi { get; set; }


        public string profesion { get; set; }
        public string estaFami { get; set; }

        public List<ConocidoPor> conoPor { get; set; }
    }
}
