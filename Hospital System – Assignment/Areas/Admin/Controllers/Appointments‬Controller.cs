using Hospital_System___Assignment.Models;
using Hospital_System___Assignment.Reposatries;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System___Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchName, int page = 1)
        {
            var query = _unitOfWork.BookAppointments.Query();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(b => b.PatientName.Contains(searchName));
            }

            query = query.OrderBy(b => b.AppointmentDate < DateTime.Now).ThenBy(b => b.AppointmentDate);

            int pagesize = 5;
            int totalPatients = query.Count();
            var BookAppointments = query.Skip((page - 1) * pagesize).Take(pagesize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPatients > 0 ? (int)Math.Ceiling(decimal.Divide(totalPatients, pagesize)) : 1;
            ViewBag.SearchName = searchName;


            return View(BookAppointments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Doctors = _unitOfWork.Doctors.Query().ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookAppointment bookAppointment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = _unitOfWork.Doctors.Query().ToList();
                return View(bookAppointment);
            }
            await _unitOfWork.BookAppointments.AddAsync(bookAppointment);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Appointment created successfully!";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var patient = await _unitOfWork.BookAppointments.GetOneAsync(b => b.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            ViewBag.Doctors = _unitOfWork.Doctors.Query().ToList();
            return View(patient);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BookAppointment bookAppointment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = _unitOfWork.Doctors.Query().ToList();
                return View(bookAppointment);
            }
            _unitOfWork.BookAppointments.Update(bookAppointment);
            TempData["Update"] = "Appointment updated successfully!";
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _unitOfWork.BookAppointments.GetOneAsync(b => b.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            _unitOfWork.BookAppointments.Remove(patient);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }

    }
}
