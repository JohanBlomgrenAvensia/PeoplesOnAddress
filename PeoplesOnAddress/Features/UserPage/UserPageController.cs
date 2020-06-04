using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplesOnAddress.Services;

namespace PeoplesOnAddress.Features.UserPage
{
    [Authorize]
    public class UserPageController : Controller
    {
        private readonly FileService _fileService;

        public UserPageController(FileService fileService)
        {
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAddressFile(IFormFile addressFile)
        {
            
            if (addressFile != null)
            {
                if (_fileService.SaveToDataBase(addressFile))
                {
                    return RedirectToAction("Index");
                }
                //TODO Return success = false view or message
                
            }
            return RedirectToAction("Index");
        }
    }
}