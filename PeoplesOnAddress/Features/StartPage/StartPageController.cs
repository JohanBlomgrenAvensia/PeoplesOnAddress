using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeoplesOnAddress.Models;

namespace PeoplesOnAddress.Features.StartPage
{
    public class StartPageController : Controller
    {
        private readonly ILogger<StartPageController> _logger;
        private readonly IServiceProvider _services;

        public StartPageController(ILogger<StartPageController> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public async Task<ActionResult> Index()
        {
            //using (var roleManager = _services.GetRequiredService<RoleManager<IdentityRole>>())
            //{
            //    var result = await roleManager.CreateAsync(new IdentityRole("Administrator"));
            //}
            //using (var userManager = _services.GetRequiredService<UserManager<IdentityUser>>())
            //{

            //    var user = await userManager.FindByNameAsync("spec4jb@hotmail.com");
            //    var result1 = await userManager.AddToRoleAsync(user, "Administrator");
            //}
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
