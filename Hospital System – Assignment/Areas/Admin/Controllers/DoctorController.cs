using Hospital_System___Assignment.Models;
using Hospital_System___Assignment.Reposatries;
using Microsoft.AspNetCore.Mvc;
namespace Hospital_System___Assignment.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchName, int page = 1)
        {
            var query = _unitOfWork.Doctors.Query();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(d => d.DoctorName.Contains(searchName));
            }

            int pagesize = 5;
            int totalDoctors = query.Count();

            var Doctor = query.Skip((page - 1) * pagesize).Take(pagesize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalDoctors > 0 ? (int)Math.Ceiling(decimal.Divide(totalDoctors, pagesize)) : 1;
            ViewBag.SearchName = searchName;


            return View(Doctor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Doctor doctor, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/doctors", fileName);

                using (var stream = System.IO.File.Create(filepath))
                {
                    image.CopyTo(stream);
                }
                doctor.DoctorImage = fileName;
            }
            await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.SaveAsync();
            TempData["Success"] = "Doctor created successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Doctor doctor, IFormFile? image, bool RemoveImage)
        {
            var olddoctor = await _unitOfWork.Doctors.GetOneAsync(d => d.DoctorId == doctor.DoctorId, IsTracked: false);

            if (olddoctor == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                doctor.DoctorImage = olddoctor.DoctorImage;
                return View(doctor);
            }
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/doctors", fileName);

                using (var stream = System.IO.File.Create(filepath))
                {
                    image.CopyTo(stream);
                }
                doctor.DoctorImage = fileName;
                if (!string.IsNullOrEmpty(olddoctor.DoctorImage))
                {

                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/doctors", olddoctor.DoctorImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            else if (RemoveImage)
            {
                doctor.DoctorImage = null;
                if (!string.IsNullOrEmpty(olddoctor.DoctorImage))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/doctors", olddoctor.DoctorImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }
            else
            {
                doctor.DoctorImage = olddoctor.DoctorImage;
            }
            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.SaveAsync();
            TempData["Update"] = "Doctor updated successfully!";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetOneAsync(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(doctor.DoctorImage))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/doctors", doctor.DoctorImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitOfWork.Doctors.Remove(doctor);
            await _unitOfWork.SaveAsync();
            return RedirectToAction("Index");
        }









    }
}
