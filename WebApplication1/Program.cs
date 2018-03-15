using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            //DbHelper.Add(new Pan { Id = 1, Name = "全部", SeqNo = 1, Status = 1 });
            // DbHelper.Add(new BuMen { Id = Util.GetCurrentTimeUnix(), Name = "test", SeqNo = 1, Status = 1 });
            //var list = DbHelper.GetAll<BuMen>();
            //foreach (var i in list)
            //{
            //    i.remark = "";
            //    DbHelper.Update(i);
            //}
            // DbHelper.SetValue("BuMen", t.Id, "Name", t.Name + "125");

            //var t = DbHelper.GetFirst<BuMen>(p => p.Id == 1515131137);
            //t.remark = "超级管理员为系统设定,无法删除";
            ////t.Status = 1;
            //DbHelper.Update(t);
            //using (var db = new LiteDatabase(@"MyData.db"))
            //{
            //var collection = db.GetCollection<Pan>(typeof(Pan).Name.ToLower());

            //collection.Delete(p => p.Status == -1);
            //var ret = collection.FindAll();
            //foreach (var r in ret)
            //{
            //    r.UserId = 1515154729;
            //    collection.Update(r);
            //}
            //}
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:9090")
                .Build();
    }
}
