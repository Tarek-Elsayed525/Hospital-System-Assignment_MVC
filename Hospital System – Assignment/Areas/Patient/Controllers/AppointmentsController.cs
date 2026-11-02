using Hospital_System___Assignment.Models;
using Hospital_System___Assignment.Reposatries;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_System___Assignment.Controllers
{
    [Area("Patient")]
    public class AppointmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(int page = 1)
        {


            var query = await _unitOfWork.BookAppointments.GetallAsync(Includes: [a => a.Doctor]);

            int pagesize = 10;
            int totalpages = query.Count();

            var appointments = query.Skip((page - 1) * pagesize).Take(pagesize).ToList();


            ViewBag.currentpage = page;
            ViewBag.TotalPages = totalpages > 0 ? (int)Math.Ceiling(decimal.Divide(totalpages, pagesize)) : 1;

            return View(appointments);
        }



        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.DoctorId == id);
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
        public async Task<IActionResult> Create(BookAppointment model)
        {
            var newAppointment = new BookAppointment
            {
                DoctorId = model.DoctorId,
                PatientName = model.PatientName,
                AppointmentDate = model.AppointmentDate,
                AppointmentTime = model.AppointmentTime
            };

            //_context.BookAppointments.Add(newAppointment);
            await _unitOfWork.BookAppointments.AddAsync(newAppointment);
            //_context.SaveChanges();
            await _unitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }



    }
}