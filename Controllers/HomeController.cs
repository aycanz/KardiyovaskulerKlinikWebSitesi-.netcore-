using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using EntityLayer.Concrete;
using KVH.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KVH.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepository());
        PatientManager pm = new PatientManager(new EfPatientRepository());
        HealthJournalManager hm = new HealthJournalManager(new EfHealthJournalRepository());

        public async Task<IActionResult> Index()
        {
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            int patientId = int.Parse(patientIdClaim.Value);
            var patient = pm.TGetById(patientId);
            if (patient == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            var healthJournal = hm.TGetListById(patientId)
                                   .OrderByDescending(h => h.Date)
                                   .FirstOrDefault() ?? new HealthJournal();

            var appointments = apm.TGetAll()
    .Where(a => a.PatientId == patientId && a.Status != "İptal Edildi") // İptal edilenleri listeleme
    .OrderByDescending(a => a.Date)
    .Take(3)
    .ToList();

            var upcomingAppointment = apm.TGetListById(patientId)
            .Where(a => a.Status == "Yaklaşan"&& a.Status!= "İptal Edildi") // Yaklaşan randevuları filtrele
            .OrderBy(a => a.Date) // Tarihe göre sırala
            .FirstOrDefault(); // İlk yaklaşan randevuyu al




            var model = new DashBoardViewModel
            {
                Patient = patient,
                HealthJournal = healthJournal,
                Appointments = appointments ?? new List<Appointment>(),

             UpcomingAppointment = upcomingAppointment // Yeni eklediğimiz veri

            };

            ViewBag.HasUpcomingAppointment = upcomingAppointment != null;


            return View(model);

        }

        public IActionResult CancelAppointment(int id)
        {
            var appointment = apm.TGetById(id);
            if (appointment != null)
            {
                appointment.Status = "İptal Edildi"; // Randevu durumunu "Cancelled" olarak güncelle
                apm.TUpdate(appointment); // Güncellenmiş randevuyu veritabanına kaydet
            }
            return RedirectToAction("Index"); // Dashboard'a geri yönlendir
        }


        [HttpPost]
        public async Task<IActionResult> SaveHealthMetrics(HealthJournal model)
        {
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            int patientId = int.Parse(patientIdClaim.Value);
            model.PatientId = patientId;
            model.Date = DateTime.Today; // Bugünün tarihini al

           

            // ModelState hatalarını kontrol et
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState Hatası: {error.ErrorMessage}");
                }
                return RedirectToAction("Index");
            }

            Console.WriteLine($"Sağlık günlüğü ekleniyor - Hasta ID: {model.PatientId}, Tarih: {model.Date}");
            await hm.TAddAsync(model);
            Console.WriteLine("Sağlık günlüğü başarıyla kaydedildi!");

            return RedirectToAction("Index");
        }



    }
}
