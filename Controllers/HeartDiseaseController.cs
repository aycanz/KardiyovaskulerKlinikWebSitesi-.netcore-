using BussinessLayer.Concrete;
using DataAccessesLayer.EntityFramework;
using EntityLayer.Concrete;
using KVH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KVH.Controllers
{
    public class HeartDiseaseController : Controller
    {
        private readonly HttpClient _httpClient;

        public HeartDiseaseController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET isteği ile formu kullanıcıya göster
        [HttpGet]
        public IActionResult Index(int appointmentId)
        {
            ViewBag.AppointmentId = appointmentId;

            return View();
        }

        // POST isteği ile formu gönder
        [HttpPost]
        public async Task<IActionResult> PredictRisk(RiskInputViewModel model)
        {
 
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5000/predict", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var predictionResult = JsonConvert.DeserializeObject<PredictionResult>(result);

                Result newResult = new Result
                {
                    AppointmentId = model.AppointmentId,
                    //Age = model.Age,
                   // Sex = model.Sex,
                    RestingBP = model.RestingBP,
                    Cholesterol = model.Cholesterol,
                    FastingBS = model.FastingBS,
                    MaxHR = model.MaxHR,
                    ChestPainType = model.ChestPainType,
                    RestingECG = model.RestingECG,
                    ExerciseAngina = model.ExerciseAngina,
                    ST_Slope = model.ST_Slope,
                    Oldpeak = model.Oldpeak,
                   // RiskScore = predictionResult.RiskScore // JSON'dan gelen sonuç
                };

                ResultManager rm = new ResultManager(new EfResultRepository());
                rm.TAdd(newResult);


                // RiskResult tablosuna kaydet
                RiskResultManager riskResultManager = new RiskResultManager(new EfRiskResultRepository());

                // Önce appointment'dan PatientId'yi al
                AppointmentManager appointmentManager = new AppointmentManager(new EfAppointmentRepository());
                var appointment = appointmentManager.TGetById(model.AppointmentId);

                RiskResult riskResult = new RiskResult
                {
                    PatientId = appointment.PatientId, // Appointment'dan PatientId'yi alıyoruz
                    AnalysisDate = DateTime.Now,
                    RiskLevel = predictionResult.Risk ? "Yüksek Risk" : "Düşük Risk", // Risk boolean değerine göre
                    Description = $"Model tarafından hesaplanan kardiyovasküler risk sonucu. Olasılık: {predictionResult.Probability:P2}"
                };

                riskResultManager.TAdd(riskResult);

                return RedirectToAction("Index", "Lab");

                // return View("PredictRisk", predictionResult);  // Sonuçları burada döndürüyoruz
            }
            else
            {
                return View("Error");  // Hata durumu
            }
        }

    }

}

