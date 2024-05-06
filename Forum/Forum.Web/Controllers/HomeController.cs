using Forum.Shared.Localizations;
using Forum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DemoProject.Web.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel();
            var errorMessage = Encoding.UTF8.GetString(HttpContext.Session.Get("ErrorMessage"));

            if (errorMessage != null)
                model.ErrorMessage = errorMessage;
            else
                model.ErrorMessage = ErrorMessages.UnhandledErrorOccurred;

            return View(model);
        }
    }
}