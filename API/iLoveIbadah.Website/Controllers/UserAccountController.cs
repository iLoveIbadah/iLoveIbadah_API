using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iLoveIbadah.Website.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public UserAccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: UserAccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterVM registration)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var isCreated = await _authenticationService.Register(registration);
                if (isCreated)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
            return View(registration);
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        // POST: UserAccountController/Create
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticationService.Authenticate(login.Email, login.PasswordHash);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError("", "Log in Attempt Failed. Please try again.");
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authenticationService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}
