using BuilkyWebRazor_Temp.Data;
using BuilkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuilkyWebRazor_Temp.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		protected readonly ApplicationDbContext _db;
		public Category? Category { get; set; }
		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult OnGet(int? id)
		{
			if (id != null && id != 0)
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
			Category? obj = _db.categories.Find(Category.Id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToPage("Index");
		}
	}
}
