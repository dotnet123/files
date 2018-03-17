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
    public class UserPage2Model : PageModelBase
    {
    public List<BuMen> buMenLst;
    public void OnGet()
    {
        checklogin();
        buMenLst = DbHelper.GetAll<BuMen>().OrderByDescending(p => p.SeqNo).ToList();
    }
    }
}