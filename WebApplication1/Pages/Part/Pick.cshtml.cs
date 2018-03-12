using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Biz;

namespace WebApplication1.Pages.Part
{
    public class PickModel : PageModelBase
    {
        public List<BuMen> buMenLst;
        public List<User> userLst;
        public long panId2;
        public long[] roles;
        public string type2;
        public void OnGet(long panId,string type)
        {
            checklogin();
            buMenLst = DbHelper.GetAll<BuMen>().OrderByDescending(p => p.SeqNo).ToList();
            userLst = DbHelper.GetAll<User>().OrderByDescending(p => p.SeqNo).ToList();
            panId2 = panId;
            type2 = type;
            var pan = DbHelper.GetFirst<Pan>(p=>p.Id== panId);
            if(type=="1")
            roles = pan.Roles1?? new long[]{};
            if (type == "2")
                roles = pan.Roles2 ?? new long[] { };
            if (type == "3")
                roles = pan.Roles3 ?? new long[] { };
        }
    }
}