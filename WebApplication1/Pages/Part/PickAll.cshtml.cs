using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages.Part
{
    public class PickAllModel : PageModelBase
    {
        public long panId2;
        public void OnGet(long panId)
        {
            checklogin();
            panId2 = panId;
        }
    }
}