using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class PageModelBase : PageModel
    {
        public User user { get; set; }
        public bool isAdmin { get; set; }
        public void checklogin()
        {
            user = UserHelper.getcurrUser(Request);
            if (user == null)
            {
                Response.Redirect("login.html", false);
                
            }
            isAdmin = UserHelper.isAdmin(Request);
        }


    }

    public class HomeModel : PageModelBase
    {
        public void OnGet()
        {
            checklogin();

        }
    }
}