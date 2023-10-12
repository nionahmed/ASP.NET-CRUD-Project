using AspNetCoreHero.ToastNotification.Abstractions;
using Employee360.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace Employee360.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyf;


        public HomeController(IConfiguration configuration, INotyfService notyf)
        {
            _configuration = configuration;
            _notyf = notyf;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ToAdminDashboard(AdminLoginModel model)
        {
            var email = _configuration["AdminCredentials:Email"];
            var password = _configuration["AdminCredentials:Password"];

            if (model.Email != email || model.Password != password)
            {
                // Authentication successful
                return View("AdminDashboard");
            }

            // Authentication failed
            _notyf.Error("Invalid Credentials");
            return View("Login");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}