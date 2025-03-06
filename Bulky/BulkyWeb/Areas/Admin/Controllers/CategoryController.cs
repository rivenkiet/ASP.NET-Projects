using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        /*public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            *//*Category? categoryFromDb1 = _db.categories.FirstOrDefault(u => u.Id == id);
			Category? categoryFromDb2 = _db.categories.Where(u => u.Id == id).FirstOrDefault();*//*
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
*/
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = categories });
        }
        [HttpDelete]
        public IActionResult Delete(int ?id)
        {
            var categoryToDeleted = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryToDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").Where(u => u.CategoryId == id).ToList();
            products.ForEach(productToDeleted =>
            {
                if (productToDeleted == null)
                {
                    
                    return;
                }
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                                    productToDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitOfWork.Product.Remove(productToDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Product deleted successfully";
            });
            _unitOfWork.Category.Remove(categoryToDeleted);
            _unitOfWork.Save();
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}

        