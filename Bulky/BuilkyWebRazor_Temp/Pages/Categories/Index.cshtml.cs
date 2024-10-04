using BuilkyWebRazor_Temp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BuilkyWebRazor_Temp.Models;

namespace BuilkyWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        protected readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
            CategoryList = _db.categories.ToList();
        }
        public void OnGet()
        {
        }
    }
}
