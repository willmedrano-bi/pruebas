using Library.Models.DTOs.User
    ;
using Library.Models.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WA_DATA.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected Library.Models.Unificada.UnificadaSpkeyContext _unificada;
        protected Library.Models.RRHH.RrhhPmiContext _rrhh;
        protected UserViewDTO user;
        protected Empleado empleado;
        // protected UserViewDTO user;


        //  protected string 

        public BaseController()
        {
            _unificada = new Library.Models.Unificada.UnificadaSpkeyContext();
            _rrhh = new Library.Models.RRHH.RrhhPmiContext();
            empleado = new Empleado();
;           user = new UserViewDTO();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            user = new UserViewDTO
            {
                id_usuario = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                user_name = User.FindFirstValue("preferred_username"),
                correo = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue("email")
            };

            
            user.correo = user.correo;

            empleado = _rrhh.Empleados.FirstOrDefault(x =>
                x.CorreoInstitucional == user.correo &&
                x.IdEstadoUsuario == 1);

            ViewBag.Empleado = empleado;
        }
    }
}