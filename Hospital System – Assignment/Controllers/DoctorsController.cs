using Hospital_System___Assignment.Date;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System___Assignment.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        public IActionResult Doctor(string searchName, string searchSpecialization, int page = 1)
        {

            var query = _context.Doctors.AsQueryable();

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