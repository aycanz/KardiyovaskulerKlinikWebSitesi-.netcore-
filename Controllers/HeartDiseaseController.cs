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
        public IActionResult Index()
        {
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
                return View("PredictRisk", predictionResult);  // Sonuçları burada döndürüyoruz
            }
            else
            {
                return View("Error");  // Hata durumu
            }
        }
    }

}
