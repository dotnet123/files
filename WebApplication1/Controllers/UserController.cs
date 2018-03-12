using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : ControllerBase
    {
        [Route("/ajax/user/login")]
        [HttpPost]
        public IActionResult Login(string userid, string pwd,bool ck)
        {
           var user=  DbHelper.GetFirst<User>(p => p.UserId == userid.Trim().ToLower() && p.PWD == pwd.Trim().ToLower());
            if (user == null)
            {
                return Json(new { code = 1, msg = "工号或密码错误!" });
            }
            Response.Cookies.Append("userid", userid, new CookieOptions{ Expires = DateTime.UtcNow.AddDays(30)});
           
            //Response.Cookies.Append("pwd", pwd, new CookieOptions { Expires = new DateTimeOffset(DateTime.Now, TimeSpan.FromDays(30)) });

            Response.Cookies.Append("uid", user.Id.ToString(), new CookieOptions{ Expires = DateTime.UtcNow.AddDays(30)});

            var ret2 = new { code = 0, msg = "登录成功" };

            return Json(ret2);
        }

        [Route("/ajax/user/resetpwd")]
        [HttpPost]
        public IActionResult reset(long id, string pwd, string newpwd)
        {
            var user = DbHelper.GetFirst<User>(p => p.Id == id );
            if (user == null)
            {
                return Json(new { code = 1, msg = "用户不存在!" });
            }
            if (user.PWD != pwd)
            {
                return Json(new { code = 1, msg = "原密码不对!" });
            }
            user.PWD = newpwd;
            DbHelper.Update(user);
            var ret2 = new { code = 0, msg = "密码修改成功" };

            return Json(ret2);
        }
        [Route("/ajax/user/out")]
        [HttpGet]
        public IActionResult out2()
        {
            Response.Cookies.Delete("uid");

            var ret2 = new { code = 0, msg = "退出成功" };

            return Json(ret2);
        }
    }
}