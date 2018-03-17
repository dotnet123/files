using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Biz;

namespace WebApplication1.Pages.Notice
{
    public class showModel : PageModelBase
    {
        public Models.Notice currNotice;
     

        public void OnGet(long id)
        {
            if (id <= 0)
            {
                // RedirectToPage("/notice/edit?id="+Util.GetCurrentTimeUnix());
                Response.Redirect("/notice/edit?id=" + Util.GetCurrentTimeUnix());
            }

            checklogin();
            currNotice = DbHelper.GetFirst<Models.Notice>(c => c.Id == id) ?? new Models.Notice()
            {
                Id = Util.GetCurrentTimeUnix(),
                ctime = DateTime.Now,
                UserId = user.Id
            };
        }
    }
}