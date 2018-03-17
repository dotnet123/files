using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApplication1.Biz;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Pages.Customer
{
    public class ListModel : PageModelBase
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetGetListAsync(long customerId)
        {
            var lst = DbHelper.Get<Models.Customer>(p=>p.Status==0)?? new List<Models.Customer>();

            lst = lst.OrderByDescending(p => p.LogTime);

            return new JsonResult(new { code = 0, msg = "保存成功", data= lst }, new JsonSerializerSettings { Converters = new List<JsonConverter> { new CustomJsonConverter { PropertyNameCase = ConverterPropertyNameCase.Ming } } });
        }
    }
}