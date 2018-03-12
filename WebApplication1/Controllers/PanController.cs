using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Biz;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class PanController : ControllerBase
    {
        [Route("/ajax/pan")]
        [HttpGet]
        public IActionResult Ajax(long parentId)
        {
           // DbHelper.Remove<Pan>(P=>P.Id>0);
            parentId = parentId == 0 ? 1 : parentId;
            var lst = DbHelper.GetAll<Pan>();
             lst = lst.Where(p=>p.ParentId==parentId).OrderByDescending(p => p.SeqNo).ToList();


            lst = lst.Where(p => p.Status == 1).ToList();

            var lstall = new List<Pan>();

            var lst1 = lst.Where(p => string.IsNullOrEmpty(p.Ext)).OrderByDescending(p => p.SeqNo).ThenByDescending(p=>p.LastTime);
            var lst2 = lst.Where(p => !string.IsNullOrEmpty(p.Ext)).OrderByDescending(p => p.SeqNo).ThenByDescending(p => p.LastTime);

            lstall.AddRange(lst1);
            lstall.AddRange(lst2);
            var ret2 = new
            {
                code = 0,
                msg = "",
                count = 30,
                data = lstall
            };

            return Json(ret2, new JsonSerializerSettings { Converters = new List<JsonConverter> { new CustomJsonConverter { PropertyNameCase = ConverterPropertyNameCase.Ming } } });
        }
        [Route("/ajax/pan/add")]
        [HttpGet]
        public IActionResult bumenadd(string name, long parentId)
        {
            DbHelper.Add(new Pan() { Id = Util.GetCurrentTimeUnix(), Name = name,
                SeqNo = 1, Status = 1, ParentId= parentId,
                LastTime=DateTime.Now,
                 Ext="",
                  Size=0,
                   Roles1=new long[] { },
                Roles2 = new long[] { },
                Roles3 = new long[] { },
                UserId = UserHelper.getcurrUserId(Request)
                     
            });

            var ret2 = new { code = 0, msg = "新增成功" };

            return Json(ret2);
        }
        [Route("/ajax/pan/updateroles")]
        [HttpGet]
        public IActionResult bumenadd(long panid, string userids, string type)
        {
            userids = userids ?? "";
            var pan= DbHelper.GetFirst<Pan>(p => p.Id == panid);

            var user = userids.Split(",",StringSplitOptions.RemoveEmptyEntries);
            var users = new List<long>();
            foreach (var u in user)
            {
                if (long.TryParse(u, out long _u)) {
                    users.Add(_u);
                }
            }
            if(type=="1")
            pan.Roles1 = users.ToArray();
            if (type == "2")
                pan.Roles2 = users.ToArray();
            if (type == "3")
                pan.Roles3 = users.ToArray();
            DbHelper.Update(pan);
            var ret2 = new { code = 0, msg = "更改成功" };

            return Json(ret2);
        }

        [Route("/ajax/pan/path")]
        [HttpGet]
        public IActionResult path(long parentid)
        {
            var pans = DbHelper.GetAll<Pan>();

            var nav = new List<string>();

            var parentid2 = parentid;
            while (true)
            {
                var pan = pans.FirstOrDefault(p => p.Id == parentid2);

                if (pan == null ||  pan.ParentId == 0)
                {
                    break;
                }
                parentid2 = pan.ParentId;
                var name = pan.Name;
                if (name.Length>6)
                { name = name.Substring(0, 5) + ".."; }
                nav.Add($@"<li class='path'>
                    <a href='javascript:void(0);' onclick='onchage2(this, ""pan?parentId={pan.Id}"");'><i class='layui-icon'>&#xe602;</i> {name}</a>
                    </li>");
            }

            var arr = nav.ToArray();
            Array.Reverse(arr);
            var ret2 = new { code = 0, msg =string.Join("", arr) };

            return Json(ret2);
        }


        [Route("/ajax/pan/roles")]
        [HttpGet]
        public IActionResult panrole(long parentid)
        {

            var ret= HasRole(parentid);
            var ret2 = new { code = 0, msg = "", data= ret };

            return Json(ret2);
        }
    }
}