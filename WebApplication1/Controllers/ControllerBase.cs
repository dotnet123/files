using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ControllerBase : Controller
    {
        public User GetCurrUser()
        {
             Request.Cookies.TryGetValue("uid", out string uidStr);
            long.TryParse(uidStr, out long uid);

            var user = DbHelper.GetFirst<User>(p => p.Id == uid);

            if (user == null)
            {
                throw  new Exception("没有登录");
            }
            return user;
        }

        public Tuple<bool,bool,bool> HasRole(long panId)
        {
           
            var pan = DbHelper.GetFirst<Pan>(p => p.Id == panId);

            if (UserHelper.isAdmin(Request))//|| pan.ParentId==0
            {
                return new Tuple<bool, bool, bool>(true, true, true);
            }

            Request.Cookies.TryGetValue("uid", out string uidStr);
            long.TryParse(uidStr, out long uid);


            var b1 = false;
            var b2 = false;
            var b3 = false;

            if (pan.Roles1?.Contains(uid)== true) b1 = true;
            if (pan.Roles2?.Contains(uid) == true) b2 = true;
            if (pan.Roles3?.Contains(uid) == true) b3 = true;
            return  new Tuple<bool, bool, bool>(b1, b2, b3);
        }

    }
}