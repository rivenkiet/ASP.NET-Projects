﻿using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
		{
			List<Category> categories = _db.categories.ToList();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Name mustnot exactly match Display Order");
			}
			if (ModelState.IsValid)
			{
				_db.categories.Add(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();

		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.categories.Find(id);
			/*Category? categoryFromDb1 = _db.categories.FirstOrDefault(u => u.Id == id);
			Category? categoryFromDb2 = _db.categories.Where(u => u.Id == id).FirstOrDefault();*/
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.categories.Update(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();

		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.categories.Find(id);
			/*Category? categoryFromDb1 = _db.categories.FirstOrDefault(u => u.Id == id);
			Category? categoryFromDb2 = _db.categories.Where(u => u.Id == id).FirstOrDefault();*/
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category? obj = _db.categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.categories.Remove(obj);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}
