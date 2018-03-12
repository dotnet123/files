using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AjaxController : Controller
    {
        [Route("/ajax/bumen")]
        [HttpGet]
        public IActionResult Ajax()
        {
            var t = DbHelper.GetAll<BuMen>().OrderByDescending(p=>p.SeqNo).ToList();
#if !DEBUG 
            t = t.Where(p => p.Status == 1).ToList();
#endif

            var ret2 = new
            {
                code =0,
                msg = "",
                count = 30,
                data = t
            };
        
            return Json(ret2,new JsonSerializerSettings{ Converters=new List<JsonConverter>{ new CustomJsonConverter { PropertyNameCase = ConverterPropertyNameCase.Ming } } });
        }

        [Route("/ajax/bumen/add")]
        [HttpGet]
        public IActionResult bumenadd(string name)
        {
            DbHelper.Add(new BuMen(){ Id=Util.GetCurrentTimeUnix(), Name = name, SeqNo = 1, Status = 1});

            var ret2 = new  { code = 0,  msg = "更新成功"  };

            return Json(ret2);
        }
        [Route("/ajax/user")]
        [HttpGet]
        public IActionResult user()
        {
            var users = DbHelper.GetAll<User>().ToList();
#if !DEBUG 
            users = users.Where(p => p.Status == 1).ToList();
#endif

            var bumen = DbHelper.GetAll<BuMen>();

            foreach (var user2 in users)
            {
                var bm = bumen.FirstOrDefault(p => p.Id == user2.BuMenId);
                if (bm !=null)
                {
                    user2.SeqNoX = user2.SeqNo * (bm.SeqNo <= 0 ? 1 : bm.SeqNo);
                }
            }

            users= users.OrderByDescending(p => p.SeqNoX).ThenByDescending(p => p.SeqNo).ToList();
            var ret2 = new
            {
                code = 0,
                msg = "",
                count = 30,
                data = users
            };

            return Json(ret2, new JsonSerializerSettings { Converters = new List<JsonConverter> { new CustomJsonConverter { PropertyNameCase = ConverterPropertyNameCase.Ming } } });
        }
        [Route("/ajax/user/add")]
        [HttpGet]
        public IActionResult useradd(string userid)
        {
            DbHelper.Add(new User { Id = Util.GetCurrentTimeUnix(),UserId = userid,Name = "", PWD = "123456",SeqNo = 1, Status = 1 });

            var ret2 = new { code = 0, msg = "更新成功" };

            return Json(ret2);
        }
        [Route("/ajax/fieldupdate")]
        [HttpGet]
        public IActionResult fieldupdate()
        {
            string table = Request.Query["table"];
            string id = Request.Query["id"];
            string key = Request.Query["key"];
            string value = Request.Query["value"];

            if (id.Contains(","))
            {
                var ids = id.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var i in ids)
                {
                    DbHelper.SetValue(table, i, key, value);
                }
            }
            else
            {
                DbHelper.SetValue(table, id, key, value);
            }
        
            var ret2 = new
            {
                code = 0,
                msg = "更新成功"

            };

            return Json(ret2);
        }


        [Route("/ajax/field/delete")]
        [HttpGet]
        public IActionResult fielddelete(string name,long id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var collection = db.GetCollection(name.ToLower());
                var k = new BsonValue(id);
                //var v = collection.FindById(k);
                collection.Delete(k);
            }
          

            var ret2 = new
            {
                code = 0,
                msg = "删除成功"

            };

            return Json(ret2);
        }
    }
}