using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public async Task<IActionResult> OnPostMingAsync(IFormFile file)
        {
            var mm = "";
            return null;
        }
        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
