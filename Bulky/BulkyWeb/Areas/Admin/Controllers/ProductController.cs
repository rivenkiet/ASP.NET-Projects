using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
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
					string productPath = Path.Combine(wwwRootPath, @"image\product");
					using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create ))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ImageUrl = @"image\product\" + fileName;
				}
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
