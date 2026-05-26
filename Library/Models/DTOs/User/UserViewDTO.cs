using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace   Library.Models.DTOs.User
{
    public class UserViewDTO
    {
        public Guid id_usuario { get; set; }
        public string user_name { get; set; }

        public string correo { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
    }
}
