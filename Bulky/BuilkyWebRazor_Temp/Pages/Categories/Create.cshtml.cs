using BuilkyWebRazor_Temp.Data;
using BuilkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuilkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        protected readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
			if (ModelState.IsValid)
			{
				_db.categories.Add(Category);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully";
				return RedirectToPage("Index");
			}
			return Page();
		}

    }
}
