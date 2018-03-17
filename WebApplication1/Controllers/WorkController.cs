using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class WorkController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Work> works;

            var user = UserHelper.getcurrUser(Request);
            if (user.SeqNo == 10000)
            {
                works = DbHelper.GetAll<Work>();
            }
            else
            {
                works = DbHelper.Get<Work>(w => w.CreatedId == user.Id);
            }

            var model = works.OrderByDescending(w => w.WorkDate).ToList();
            model.ForEach(w => {
                w.Creator = DbHelper.GetFirst<User>(u => u.Id == w.CreatedId);
            });

            return Json(new { code = 0,
                count = model.Count,
                data = model.Select(s => 
                    new { id = s.Id, work_date = s.WorkDate.ToString("yyyy-MM-dd"), content = s.Content })
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var model = DbHelper.GetFirst<Work>(w => w.Id == id);
            return Json(model);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Work model)
        {
            model.Id = Util.GetCurrentTimeUnix();
            model.CreatedId = UserHelper.getcurrUserId(Request);
            model.CreatedDate = DateTime.Now;

            DbHelper.Add(model);

            return Json(new { code = 0 });
        }

        [Route("edit")]
        [HttpPost]
        public IActionResult Edit([FromBody]Work model)
        {
            var work = DbHelper.GetFirst<Work>(w => w.Id == model.Id);
            work.WorkDate = model.WorkDate;
            work.Content = model.Content;
            work.UpdatedDate = DateTime.Now;

            DbHelper.Update(work);

            return Json(new { code = 0 });
        }

        [Route("delete")]
        [HttpPost]
        public IActionResult Delete(long id)
        {
            DbHelper.Remove<Work>(w => w.Id == id);

            return Json(new { code = 0 });
        }
    }
}
