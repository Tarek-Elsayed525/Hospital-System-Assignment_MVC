using Hospital_System___Assignment.Reposatries;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System___Assignment.Controllers
{
    [Area("Patient")]
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Doctor(string searchName, string searchSpecialization, int page = 1)
        {

            var query = _unitOfWork.Doctors.Query();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(d => d.DoctorName.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchSpecialization))
            {
                query = query.Where(d => d.Specialization.Contains(searchSpecialization));
            }

            int pageSize = 5;
            int totalDoctors = query.Count();

            var doctors = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalDoctors > 0 ? (int)Math.Ceiling(decimal.Divide(totalDoctors, pageSize)) : 1;
            ViewBag.SearchName = searchName;
            ViewBag.SearchSpecialization = searchSpecialization;

            return View(doctors);
        }
    }
}