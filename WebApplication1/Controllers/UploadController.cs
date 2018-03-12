using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Biz;
using WebApplication1.Controllers;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1
{
    public class UploadController : Controller
    {
        public static string CheckFile()
        {

            //命名一个今天的文件夹
            string folder = DateTime.Now.ToString("yyyyMMdd");

            //判断文件是否存在
            if (!System.IO.Directory.Exists("Uploads/" + folder))
            {
                //自动生成文件夹
                System.IO.Directory.CreateDirectory("Uploads/" + folder);

                //生成后返回文件夹名
                return "Uploads/" + folder+"/";
            }

            //如果存在，直接返回今天的文件夹名
            return "Uploads/" + folder + "/";
        }

        [Route("/tools/Upload")]
        [HttpPost]
        public IActionResult UploadFilesAjax()
            {
                long size = 0;
                var files = Request.Form.Files;
                string parentId = Request.Form["parentId"];

            foreach (var file in files)
                    {

                        var filename = ContentDispositionHeaderValue
                                        .Parse(file.ContentDisposition)
                                         .FileName
                                         .Trim('"');
                var fext = filename.Substring(filename.IndexOf('.')+1, filename.Length - filename.IndexOf('.')-1);
                var fname = filename.Substring(0, filename.IndexOf('.'));
             
                filename = CheckFile()+ Guid.NewGuid().ToString() + "." + fext;
                        size += file.Length;
                         using (FileStream fs = System.IO.File.Create(filename))
                             {
                               file.CopyTo(fs);
                               fs.Flush();
                             }
             
                DbHelper.Add(new Pan()
                {
                    Id = Util.GetCurrentTimeUnix(),
                    Name = fname,
                    Url= filename,
                    SeqNo = 1,
                    Status = 1,
                    ParentId = Convert.ToInt64(parentId),
                    LastTime = DateTime.Now,
                    Ext = fext,
                    Size = size,
                    Roles1 = new long[] { },
                    Roles2 = new long[] { },
                    Roles3 = new long[] { },
                    UserId = UserHelper.getcurrUserId(Request)

                });
            }

            var ret= new
                {
                    code= 0,
                    msg="",
                    src="http://cdn.layui.com/123.jpg"
                
                };

                 return Json(ret);
             }

     
    }
}
