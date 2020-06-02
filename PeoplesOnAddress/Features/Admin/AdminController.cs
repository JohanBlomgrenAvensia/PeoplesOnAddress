using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonsOnAddress.Services;

namespace PersonsOnAddress.Features.Admin
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly CompanyService _companyService;

        public AdminController(CompanyService companyService)
        {
            _companyService = companyService;
        }
        public IActionResult Index()
        {
            var model = new AdminViewModel();
            model.Company = new Company();
            return View(model);
        }
        public ActionResult CreateCompany(AdminViewModel model)
        {
            _companyService.CreateCompany(model.Company.Name);
            return RedirectToAction("Index");
        }

        public ActionResult CreateUSer(AdminViewModel model)
        {

        }

    }
}