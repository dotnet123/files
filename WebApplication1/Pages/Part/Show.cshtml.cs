using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Biz;
using WebApplication1.Models;

namespace WebApplication1.Pages.Part
{
    public class ShowModel : PageModel
    {
        public Pan pan;
        public int type;
       
        public void OnGet(long id)
        {

            pan = DbHelper.GetFirst<Pan>(p => p.Id == id);
            switch (pan.Ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                case "gif":
                case "png":
                case "bmp":
                    type = 1;
                    break;
                case "doc":
                case "docx":
                case "ppt":
                case "pptx":
                case "xls":
                case "xlsx":
                case "txt":
                    type = 2;
                    break;
            }
         
        }
    }
}