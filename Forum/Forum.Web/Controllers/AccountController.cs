using Forum.Application.Accounts.Interfaces;
using Forum.Application.Accounts.Requests;
using Forum.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginRequestModel model)
        {
            if (!ModelState.IsValid)
                return View();

            await _accountService.SignInAsync(model);

            if (await _accountService.IsAdminAsync(model.Username, UserRole.Admin))
                return RedirectToAction("Topics", "Topic", new { area = "Admin" });

            return RedirectToAction("Topics", "Topic");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterRequestModel model)
        {
            if (!ModelState.IsValid)
                return View();

            await _accountService.RegisterAsync(model);

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("Topics", "Topic");
        }
    }
}
