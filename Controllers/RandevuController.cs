using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KVH.Controllers
{
    public class RandevuController : Controller
        
    {
        DoctorManager doctorManager =new DoctorManager(new EfDoctorRepository());
        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepository());



        public IActionResult MyCurrentAppointment()
        {
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            int patientId = int.Parse(patientIdClaim.Value);
            var upcomingAppointments = apm.TGetAll()
       .Where(a => a.PatientId == patientId && a.Date >= DateTime.Now && a.Status != "İptal Edildi")
       .ToList();

            return View(upcomingAppointments);
        }

        public IActionResult MyOldAppointment()
        {
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            int patientId = int.Parse(patientIdClaim.Value);

            var pastAppointments = apm.TGetAll()
                .Where(a => a.PatientId == patientId && a.Date < DateTime.Now)
                .ToList();

            return View(pastAppointments);
        }


        [HttpGet]
        public IActionResult NewAppointment()
        {
            List<SelectListItem> values = new List<SelectListItem>();

             values.Add(new SelectListItem
            {
                Text = "Doktor Seçin",
                Value = "",
                Selected = true
            });

             values.AddRange(from x in doctorManager.TGetAll()
                            select new SelectListItem
                            {
                                Text = "Dr. " + x.FirstName + " " + x.LastName,
                                Value = x.DoctorId.ToString()
                            });

            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public IActionResult NewAppointment(Appointment p) {
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login"); // Kullanıcı giriş yapmadıysa girişe yönlendir
            }

            int patientId = int.Parse(patientIdClaim.Value);
            p.PatientId = patientId; // Giriş yapan hastanın ID'sini ata

            // Randevu saati ve doktoru kontrol et
            var existingAppointment = apm.TGetAll().FirstOrDefault(a => a.DoctorId == p.DoctorId && a.Date.Date == p.Date.Date && a.Time == p.Time);

            // Eğer daha önce aynı doktor ve saatte randevu varsa, hata mesajı ver
            if (existingAppointment != null)
            {
                TempData["ErrorMessage"] = "Bu saatte o doktora ait bir randevu zaten mevcut!";
                return RedirectToAction("NewAppointment"); // Randevu formunu tekrar göster
            }

            // Gelecek randevular için "Aktif", geçmiş randevular için "Geçmiş" yaz
            if (p.Date >= DateTime.Now)
            {
                p.Status = "Yaklaşan";  // Gelecek randevu
            }
            else
            {
                p.Status = "Tamamlandı";  // Geçmiş randevu
            }

            if (string.IsNullOrEmpty(p.Notes))
            {
                p.Notes = "Not belirtilmedi";
            }
            apm.TAdd(p);
            return RedirectToAction("MyCurrentAppointment");

        }


        public IActionResult CancelAppointment(int id)
        {
            var appointment = apm.TGetById(id);
            if (appointment != null)
            {
                appointment.Status = "İptal Edildi"; // Randevu durumunu değiştir
                apm.TUpdate(appointment); // Güncellenmiş randevuyu kaydet
            }
            return RedirectToAction("MyCurrentAppointment"); // Güncel randevulara yönlendir
        }


        // AJAX ile mevcut randevu kontrolü yapmak için
        public JsonResult CheckAppointmentAvailability(int doctorId, DateTime date, TimeSpan time)
        {
            var existingAppointment = apm.TGetAll()
                .FirstOrDefault(a => a.DoctorId == doctorId && a.Date.Date == date.Date && a.Time == time && a.Status != "İptal Edildi");

            return Json(existingAppointment == null);  // Eğer boşsa true, varsa false döner
        }

        // Doktor için müsait tarihleri döndürür
        public JsonResult GetAvailableDates(int doctorId)
        {
            // Hasta ID'sini al
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return Json(new List<string>());
            }

            int patientId = int.Parse(patientIdClaim.Value);

            // Bugünden itibaren 30 günlük tarih aralığı oluşturalım
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(5);

            // Doktorun mevcut randevularını çekelim
            var existingAppointments = apm.TGetAll()
                .Where(a => a.DoctorId == doctorId && a.Status != "İptal Edildi")
                .ToList();

            // Hastanın bu doktorla olan randevularını çekelim
            var patientAppointmentsWithDoctor = apm.TGetAll()
                .Where(a => a.DoctorId == doctorId && a.PatientId == patientId && a.Status != "İptal Edildi")
                .ToList();

            // Müsait tarihler için liste oluşturalım
            var availableDates = new List<string>();

            // Tarih aralığı için döngü oluşturalım
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Hafta sonu ve tatil günleri kontrolü
                if (date.DayOfWeek != DayOfWeek.Sunday) // Pazar günleri hariç
                {
                    // Hastanın aynı gün içinde aynı doktordan başka randevusu var mı?
                    bool hasExistingAppointment = patientAppointmentsWithDoctor.Any(a => a.Date.Date == date.Date);

                    // Eğer hasta zaten aynı gün için randevu almışsa, bu tarihi atlayalım
                    if (hasExistingAppointment)
                    {
                        continue;
                    }

                    // Bu tarihte doktorun toplam randevu sayısı
                    var appointmentsOnDate = existingAppointments
                        .Count(a => a.Date.Date == date.Date);

                    // Eğer doktorun günlük maksimum randevu sayısından az randevusu varsa
                    // (Örneğin, günde maksimum 10 randevu alabiliyorsa)
                    if (appointmentsOnDate < 10)
                    {
                        availableDates.Add(date.ToString("yyyy-MM-dd"));
                    }
                }
            }

            return Json(availableDates);
        }

        // Seçilen tarih için müsait saatleri döndürür
        public JsonResult GetAvailableTimes(int doctorId, string date)
        {
            // Tarih değerini parse edelim
            DateTime selectedDate = DateTime.Parse(date);

            // Doktorun seçilen tarihteki randevularını çekelim
            var existingAppointments = apm.TGetAll()
                .Where(a => a.DoctorId == doctorId && a.Date.Date == selectedDate.Date && a.Status != "İptal Edildi")
                .ToList();

            // Doktorun çalışma saatleri (örnek olarak 09:00-17:00)
            var workStartTime = TimeSpan.Parse("09:00");
            var workEndTime = TimeSpan.Parse("17:00");

            // Randevu süresini 30 dakika olarak belirleyelim
            var appointmentDuration = TimeSpan.FromMinutes(30);

            // Mevcut randevularda dolu olan saatler
            var bookedTimes = existingAppointments
                .Select(a => a.Time)
                .ToList();

            // Müsait saatler için liste oluşturalım
            var availableTimes = new List<string>();

            // Çalışma saatleri içerisinde 30'ar dakikalık dilimleri kontrol edelim
            for (var time = workStartTime; time < workEndTime; time = time.Add(appointmentDuration))
            {
                // Bu saat dilimi dolu mu kontrol edelim
                if (!bookedTimes.Contains(time))
                {
                    // Müsait ise listeye ekleyelim
                    availableTimes.Add(time.ToString(@"hh\:mm"));
                }
            }

            return Json(availableTimes);
        }



    }
}
