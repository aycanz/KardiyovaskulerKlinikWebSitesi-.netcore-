using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    public class AppointmentController : Controller
    {

        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepository());

        public IActionResult Appointment()
        {
            var patientId = 2; // Burayı oturumdan veya giriş yapan hastadan almalısın.
            var appointments = apm.TGetListById(patientId); // Hastaya ait randevuları çek
            return View(appointments);
     
        }


        public PartialViewResult AppointmentListByPatient()
        {
            var values = apm.TGetListById(1);
            return PartialView("_AppointmentList", values);
        }

        [HttpPost]
        public PartialViewResult BookAppointment(Appointment appointment)
        {
            // Seçilen doktorun bu tarih ve saat için randevusu var mı?
            bool isBooked = apm.TGetAll().Any(a =>
                a.DoctorId == appointment.DoctorId &&
                a.Date == appointment.Date &&
                a.Time == appointment.Time
            );

            if (isBooked)
            {
                TempData["ErrorMessage"] = "Bu doktor için bu tarih ve saat dolu. Lütfen başka bir zaman seçin.";
                return PartialView("Appointment");
            }

            // Eğer uygun randevu varsa ekle
            appointment.Status = "Beklemede";
            apm.TAdd(appointment);
            return PartialView("Appointment");
        }



    }
}
