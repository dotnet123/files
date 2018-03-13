using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Biz;

namespace WebApplication1.Pages.Customer
{
    public class EditModel : PageModelBase
    {
        public Models.Customer customer;
        public string btTxt="新增加";

        public void OnGet(long customerid)
        {
            if (customerid>0)
            {
                btTxt = "修改";
            }
            checklogin();
            customer = DbHelper.GetFirst<Models.Customer>(c => c.Id == customerid) ?? new Models.Customer
            {
                Id= Util.GetCurrentTimeUnix(), LogTime = DateTime.Now,
                UserId = user.Id
            };
        }

        public async Task<IActionResult> OnPostSaveAsync(Models.Customer customer)
        {
           var  customer2 = DbHelper.GetFirst<Models.Customer>(c => c.Id == customer.Id);
            if (customer2 == null)
            {
         
            customer.Status = 0;
            customer.HuiKuanList = new List<Tuple<DateTime, string>>();
            customer.VisitRecordList = new List<Tuple<DateTime, string>>();
            customer.SeqNo = 1;
            DbHelper.Add(customer);
            }
            else
            {
                customer2.CompanyName = customer.CompanyName;
                customer2.Contacts = customer.Contacts;
                DbHelper.Update(customer2);
            }
          
            return new JsonResult(new { code = 0, msg = "保存成功" });
        }
    }
}