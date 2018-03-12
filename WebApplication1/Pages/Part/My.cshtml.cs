using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Pages.Part
{
    public class MyModel : PageModelBase
    {
       
        public void OnGet()
        {
            checklogin();
           
        }
    }
}