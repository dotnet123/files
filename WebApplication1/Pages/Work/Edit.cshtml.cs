using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Biz;

namespace WebApplication1.Pages.Work
{
    public class EditModel : PageModel
    {
        public Models.Work WorkViewModel { get; set; }

        public void OnGet(long id)
        {
            WorkViewModel = DbHelper.GetFirst<Models.Work>(w => w.Id == id);
        }
    }
}