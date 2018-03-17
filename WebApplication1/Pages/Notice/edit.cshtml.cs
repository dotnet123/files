using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Biz;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Pages.Notice
{
    public class editModel : PageModelBase
    {
        public Models.Notice currNotice;
        public string btTxt = "新增加";

        public void OnGet(long id)
        {
            if (id<=0)
            {
               // RedirectToPage("/notice/edit?id="+Util.GetCurrentTimeUnix());
                Response.Redirect("/notice/edit?id=" + Util.GetCurrentTimeUnix());
            }
            if (id > 0)
            {
                btTxt = "修改";
            }
            checklogin();
            currNotice = DbHelper.GetFirst<Models.Notice>(c => c.Id == id) ?? new Models.Notice()
            {
                Id = Util.GetCurrentTimeUnix(),
                ctime = DateTime.Now,
                UserId = user.Id
            };
        }

        public async Task<IActionResult> OnGetGetRecordAsync(long customerId)
        {
            var customer = DbHelper.GetFirst<Models.Customer>(c => c.Id == customerId);
            if (customer == null)
            {

                return new JsonResult(new { code = 1, msg = "请先建客户档案", v1 = new List<Record>(), v2 = new List<Record>() });
            }
            var v1 = customer.VisitRecordList ?? new List<Record>();
            var v2 = customer.HuiKuanList ?? new List<Record>();
            v1 = v1.OrderByDescending(p => p.date).ToList();
            v2 = v2.OrderByDescending(p => p.date).ToList();
            return new JsonResult(new { code = 0, msg = "保存成功", v1, v2 });
        }


        public async Task<IActionResult> OnPostSaveAsync(Models.Notice customer)
        {
            var customer2 = DbHelper.GetFirst<Models.Notice>(c => c.Id == customer.Id);



            customer.UserId = UserHelper.getcurrUserId(Request);
            if (customer2 == null)
            {

                customer.Status = 0;

                customer.SeqNo = 1;

                DbHelper.Add(customer);
            }
            else
            {
                customer2.title = customer.title;
                customer2.content = customer.content;
                customer2.ctime = customer.ctime;
            
                customer2.UserId = UserHelper.getcurrUserId(Request);
                DbHelper.Update(customer2);
            }

            return new JsonResult(new { code = 0, msg = "保存成功" });
        }
    }
}