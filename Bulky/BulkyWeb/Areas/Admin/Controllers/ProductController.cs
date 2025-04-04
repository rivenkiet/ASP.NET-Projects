﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
			//List<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
			//return View(products);
			return View();
        }

		

		public IActionResult Upsert(int? id)
        {
			ProductVM productVM = new()
			{
				Product = new Product(),
				CategoryList = _unitOfWork.Category.GetAll()
				.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				})
			};
			
			if(id == null || id == 0)
			{
				//create
				return View(productVM);
			}
			else
			{
				//Update
				productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
				return View(productVM);
			}
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						string oldFileName = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
						if(System.IO.File.Exists(oldFileName))
						{
							System.IO.File.Delete(oldFileName);
						}
						
					}
					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ImageUrl = @"\images\product\" + fileName;
				}
				else if(productVM.Product.Id == 0 && file == null) // Truong hop update khong thay hinh
				{
					productVM.Product.ImageUrl = "";
				}

				if (productVM.Product.Id == 0)
				{
					_unitOfWork.Product.Add(productVM.Product);
					TempData["success"] = "Product created succesfully";
				}
				else
				{
					_unitOfWork.Product.Update(productVM.Product);
					TempData["success"] = "Product updated succesfully";
				}
                
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
			else
			{
				productVM.CategoryList = _unitOfWork.Category.GetAll()
					.Select(u => new SelectListItem
					{
						Text = u.Name,
						Value = u.Id.ToString()
					});
				TempData["error"] = "Product created fail ";
				return View(productVM);
			}
			
        }




		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
            List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = products });
        }
		#endregion
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var productToDeleted = _unitOfWork.Product.Get(u => u.Id == id);
			if (productToDeleted == null) {
				return Json(new { success = false, message = "Error while deleting" });
			}
			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
								productToDeleted.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			_unitOfWork.Product.Remove(productToDeleted);
			_unitOfWork.Save();
			//List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new { success = true, message = "Delete successful" });
		}
    }
}
