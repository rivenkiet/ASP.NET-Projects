﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
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

			return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created succesfully";
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

		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

			return View(product);
		}
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				TempData["success"] = "Product edited succesfully";
				return RedirectToAction("Index");
			}
			TempData["error"] = "Product edited fail ";
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null || id == 0)
			{
				TempData["error"] = "Product deleted fail ";
				return NotFound();
				
			}
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if (product == null)
			{
				TempData["error"] = "Product deleted fail ";
				return NotFound();	
			}
			_unitOfWork.Product.Remove(product);
			_unitOfWork.Save();
			TempData["sucess"] = "Product deleted succesfully";
			return RedirectToAction("Index");
		}


	}
}
