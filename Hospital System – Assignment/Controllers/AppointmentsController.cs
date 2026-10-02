using Hospital_System___Assignment.Date;
using Hospital_System___Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System___Assignment.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        [HttpGet]
        public IActionResult Create(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            ViewBag.DoctorName = doctor.DoctorName;

            var model = new BookAppointment
            {
                DoctorId = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BookAppointment model)
        {
            var newAppointment = new BookAppointment
            {
                DoctorId = model.DoctorId,
                PatientName = model.PatientName,
                AppointmentDate = model.AppointmentDate,
                AppointmentTime = model.AppointmentTime
            };

            _context.BookAppointments.Add(newAppointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var appointments = _context.BookAppointments
                .Include(a => a.Doctor)
                .ToList();

            return View(appointments);
        }
    }
}