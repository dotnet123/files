using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserHelper
    {

        public static bool isAdmin(HttpRequest request)
        {

            var uid = getcurrUserId(request);

            var user = DbHelper.GetFirst<User>(p => p.Id == uid);
            if (user?.BuMenId== 1515131137)
            {
                return true;
            }
            return false;
        }

        public static bool isCanUpdateFileName(long panid, HttpRequest request)
        {
            ControllerBase cb = new ControllerBase();
            var pan = DbHelper.GetFirst<Pan>(p => p.Id == panid);
            var uid = getcurrUserId(request);
            if (isAdmin(request))
                return true;
            if (pan.Roles1!=null &&pan.Roles1.Contains(uid))
            {
                return true;
            }
            return false;
        }
        public static User getcurrUser(HttpRequest request)
        {

            var uid = getcurrUserId(request);
            var user = DbHelper.GetFirst<User>(p => p.Id == uid);
            return user;
        }
        public static long getcurrUserId(HttpRequest request)
        {

            request.Cookies.TryGetValue("uid", out string uidStr);
            long.TryParse(uidStr, out long uid);
          
            return uid;
        }
    }
}
