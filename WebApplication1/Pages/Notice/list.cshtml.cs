using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApplication1.Biz;
using WebApplication1.Controllers;

namespace WebApplication1.Pages.Notice
{
    public class listModel : PageModelBase
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetGetListAsync()
        {
            var lst = DbHelper.Get<Models.Notice>(p => p.Status == 0) ?? new List<Models.Notice>();

            lst = lst.OrderByDescending(p => p.ctime);

            return new JsonResult(new { code = 0, msg = "", data = lst }, new JsonSerializerSettings { Converters = new List<JsonConverter> { new CustomJsonConverter { PropertyNameCase = ConverterPropertyNameCase.Ming } } });
        }
    }
}