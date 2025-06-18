using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    public class RiskPageController : Controller
    {
        RiskResultManager rrm = new RiskResultManager(new EfRiskResultRepository());
        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepository());


        public IActionResult Index(int patientId)
        {
            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

             patientId = int.Parse(patientIdClaim.Value);

            // Bu hastanın tüm randevularını al
            var patientAppointments = apm.TGetAll()
                .Where(a => a.PatientId == patientId)
                .Select(a => a.AppointmentId)
                .ToList();

            // Bu randevulara ait tüm risk sonuçlarını çek
            var riskResults = rrm.TGetAll()
             //   .Where(r => patientAppointments.Contains(r.AppointmentId))
                .OrderByDescending(r => r.AnalysisDate)
                .ToList();

            return View(riskResults);
        }
    }
}
