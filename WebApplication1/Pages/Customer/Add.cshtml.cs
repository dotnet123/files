using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.Customer
{
    public class AddModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPostAdd()
        {
            var stream = Request.Body.ToString();

            return RedirectToPage();
        }
        
    }
}