using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Biz;
using WebApplication1.Controllers;
using WebApplication1.Models;

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

        public async Task<IActionResult> OnGetGetRecordAsync(long customerId)
        {
            var customer = DbHelper.GetFirst<Models.Customer>(c => c.Id == customerId);
            if (customer == null)
            {

                return new JsonResult(new { code = 1, msg = "请先建客户档案",v1=new List<Record>() , v2 = new List<Record>() });
            }
            var v1 = customer.VisitRecordList ?? new List<Record>();
            var v2 = customer.HuiKuanList ?? new List<Record>();
           v1= v1.OrderByDescending(p => p.date).ToList();
            v2 = v2.OrderByDescending(p => p.date).ToList();
            return new JsonResult(new { code = 0, msg = "保存成功",v1,v2 });
        }

        public async Task<IActionResult> OnPostSaveRecordAsync(Record record)
        {
            var customer = DbHelper.GetFirst<Models.Customer>(c => c.Id == record.customerId);
            if (customer == null)
            {
                return new JsonResult(new { code = 1, msg = "请先建客户档案" });
            }

            record.userid = UserHelper.getcurrUserId(Request);
            if (record.type == 1)
            {
                var v1 = customer.VisitRecordList ?? new List<Record> ();
                v1.Add(record);
                customer.VisitRecordList = v1;
            }
            else
            {
                var v1 = customer.HuiKuanList ?? new List<Record>();
                v1.Add(record);
                customer.HuiKuanList = v1;
            }
            DbHelper.Update(customer);
            return new JsonResult(new { code = 0, msg = "保存成功" });
        }
        public async Task<IActionResult> OnPostSaveAsync(Models.Customer customer)
        {
           var  customer2 = DbHelper.GetFirst<Models.Customer>(c => c.Id == customer.Id);



            customer.UserId = UserHelper.getcurrUserId(Request);
            if (customer2 == null)
            {
         
            customer.Status = 0;
    
            customer.SeqNo = 1;

            DbHelper.Add(customer);
            }
            else
            {
                customer2.CompanyName = customer.CompanyName;
                customer2.Contacts = customer.Contacts;
                customer2.HuiKuanStatus = customer.HuiKuanStatus;
                customer2.Address = customer.Address;
                customer2.Contacts = customer.Contacts;
                customer2.Tel = customer.Tel;
                customer2.UserId= UserHelper.getcurrUserId(Request);
                DbHelper.Update(customer2);
            }
          
            return new JsonResult(new { code = 0, msg = "保存成功" });
        }
    }
}