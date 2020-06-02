using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonsOnAddress.Models;

namespace PersonsOnAddress.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _services;

        public HomeController(ILogger<HomeController> logger, IServiceProvider services)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
