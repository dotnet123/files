using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Controllers;

namespace WebApplication1.Pages
{
    public class panModel : PageModel
    {
        public string parentId { get; set; }
        public bool isAdmin { get; set; }
        public bool isCanUpdateFileName { get; set; }
        public void OnGet()
        {
            parentId = Request.Query["parentId"];
            isAdmin = UserHelper.isAdmin(Request);
            parentId = string.IsNullOrEmpty(parentId) ? "1" : parentId;
            long.TryParse(parentId, out long _parentId);
            isCanUpdateFileName = UserHelper.isCanUpdateFileName(_parentId, Request);
        }
    }
}