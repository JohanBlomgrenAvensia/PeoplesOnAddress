using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace PeoplesOnAddress.Features.Admin
{
    public class AdminViewModel
    {
        public Company Company { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public User User { get; set; }

    }


    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }
}
