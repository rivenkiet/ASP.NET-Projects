using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class CompanyController : Controller
    {
        
        private IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
		
		public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int? id) {
            Company company;
            if (id == null || id == 0)
            {
                company = new Company();
                return View(company);
            }
            else
            {
				company = _unitOfWork.Company.Get(u => u.Id == id);
				return View(company);
			}
        }
        [HttpPost]
        public IActionResult Upsert(Company Company)
        {
			if(ModelState.IsValid)
            {
                if (Company.Id == 0)
                {
                    _unitOfWork.Company.Add(Company);
                }
                else
                {
                    _unitOfWork.Company.Update(Company);
                }
                _unitOfWork.Save();
                TempData["success"] = "Upsert succesfull";
                return RedirectToAction("Index");
            }
            else
            {
				TempData["error"] = "Upsert fail";
                return View(Company);
			}
            
		}
		#region API_CALLS
        public IActionResult GetAll()
        {
            List<Company> listCompany = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data = listCompany});
        }
        [HttpDelete]
        public IActionResult Delete(int? id) 
        {
            if(id == null || id == 0) {
				return Json(new { success = false, message = "Error while deleting" });
			}
            Company companyToDeleted = _unitOfWork.Company.Get(u => u.Id==id);
            if (companyToDeleted == null)
            {
				return Json(new { success = false, message = "Error while deleting" });
			}
            _unitOfWork.Company.Remove(companyToDeleted);
            _unitOfWork.Save();
			return Json(new { success = true, message = "Delete successful" });
		}
		#endregion
	}
}

	
