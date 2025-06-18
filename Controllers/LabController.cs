using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    [Authorize(Roles = "Laborant")]

    public class LabController : Controller
    {

        DoctorManager dm = new DoctorManager(new EfDoctorRepository());
        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepository());
        PatientManager pm = new PatientManager(new EfPatientRepository());
        ResultManager rm = new ResultManager(new EfResultRepository());
        public IActionResult Index()
        {
            var today = DateTime.Today;

            var appointments = apm.TGetAll()
                    .Where(a => a.Date.Date == today)
                    .Where(a => !rm.TGetAll().Any(r => r.AppointmentId == a.AppointmentId)) // Result yoksa al
                    .ToList();
            return View(appointments); // Index.cshtml

        }



    }
}
