using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    public class ResultController : Controller
    {

        DoctorManager dm = new DoctorManager(new EfDoctorRepository());
        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepository());
        PatientManager pm = new PatientManager(new EfPatientRepository());
        ResultManager rm = new ResultManager(new EfResultRepository());
 
        public async Task<IActionResult> Result()
        {

            var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientId");
            if (patientIdClaim == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            int patientId = int.Parse(patientIdClaim.Value);
            var appointmentsWithResults = apm.GetAppointmentsWithResultByPatient(patientId);


            return View(appointmentsWithResults);
        }


        public IActionResult Detail(int appointmentId)
        {
            var appointment = apm.GetAppointmentWithResult(appointmentId);


            if (appointment == null || appointment.Result == null)
            {
                return NotFound();
            }

            return View(appointment.Result);
        }



    }
}
