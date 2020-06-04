using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeoplesOnAddress.Services;
using PersonsOnAddress.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PeoplesOnAddress.Features.Admin
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly CompanyService _companyService;
        private readonly UserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            CompanyService companyService, 
            UserManager<IdentityUser> userManager, 
            UserService userService,
            ILogger<AdminController> logger)
        {
            _companyService = companyService;
            _userManager = userManager;
            _userService = userService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new AdminViewModel();
            var companies = _companyService.GetCompanies();
            model.Company = new Company();
            model.Companies = companies.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            return View(model);
        }
        public ActionResult CreateCompany(AdminViewModel model)
        {
            _companyService.CreateCompany(model.Company.Name);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CreateUser(AdminViewModel model)
        {
            var a = model;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.User.Email, Email = model.User.Email };
                var result = await _userManager.CreateAsync(user, model.User.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //TODO SEND EMAIL ECT
                    _userService.ConnectUserToCompany(model.User.Company, user.Id);



                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult GetUser(string companyId)
        {
            var users = _userService.GetUsersByCompany(companyId);
            return View(users);
        }

    }
}