using Library.Models.DTOs.Rnpn;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using WA_DATA.Models;
namespace WA_DATA.Controllers
{

    
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public async Task<IActionResult> LlamarApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("http://localhost:5052/WeatherForecast");
            var contenido = await response.Content.ReadAsStringAsync();

            ViewBag.ApiResponse = contenido;
            return View("Index");
        }
        [Authorize]
        public async Task<IActionResult> VerToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            return Content($"Access Token:\n{accessToken}\n\nID Token:\n{idToken}\n\nRefresh Token:\n{refreshToken}");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => this.View();

        

    }


}
