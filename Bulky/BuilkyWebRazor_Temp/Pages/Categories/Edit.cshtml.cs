using BuilkyWebRazor_Temp.Data;
using BuilkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuilkyWebRazor_Temp.Pages.Categories
{
	[BindProperties]
    public class EditModel : PageModel
    {
		protected readonly ApplicationDbContext _db;
		public Category? Category { get; set; }
		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult OnGet(int? id)
		{
			if(id != null && id != 0)
			{
				Category = _db.categories.FirstOrDefault(u => u.Id == id);
				return Page();
			}
			else
			{
				return NotFound();
			}
			
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid && Category != null)
			{
				_db.categories.Update(Category);
				_db.SaveChanges();
				TempData["success"] = "Category edited successfully";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
