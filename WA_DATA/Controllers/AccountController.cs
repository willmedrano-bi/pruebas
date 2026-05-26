using Microsoft.AspNetCore.Http;


using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WA_DATA.Controllers;
public class AccountController : Controller
{
    private readonly ILogger<AccountController> logger;

    public AccountController(ILogger<AccountController> logger) => this.logger = logger;

    [AllowAnonymous]
    public IActionResult SignIn()
    {
        if (!this.User.Identity!.IsAuthenticated)
        {
            return this.Challenge(OpenIdConnectDefaults.AuthenticationScheme);
        }

        return this.RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    [Authorize]
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => this.RedirectToAction("AccessDenied", "Home");
}