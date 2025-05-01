using KVH.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class HeartController : Controller
{
    private readonly HttpClient _httpClient;

    public HeartController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet]
    public IActionResult Predict()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Predict(HeartInputModel model)
    {
        var inputArray = ConvertToModelInput(model);
        var json = JsonConvert.SerializeObject(new { input = inputArray });

        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://127.0.0.1:5000/predict", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

        if (result.ContainsKey("error"))
        {
            // Eğer hata mesajı varsa, kullanıcıya hata mesajını gösterelim
            ViewBag.Result = $"Hata: {result["error"]}";
        }
        else if (result.ContainsKey("prediction"))
        {
            // Sonuç başarılı ise, tahmin sonucunu gösterelim
            var prediction = Convert.ToInt32(result["prediction"]);
            ViewBag.Result = prediction == 1 ? "Kalp hastalığı tespit edildi!" : "Kalp hastalığı yok.";
        }

        return View(model);
    }

    private List<float> ConvertToModelInput(HeartInputModel model)
    {
        var input = new List<float>();

        // Numeric
        input.Add(model.Age);
        input.Add(model.RestingBP);
        input.Add(model.Cholesterol);
        input.Add(model.FastingBS);
        input.Add(model.MaxHR);
        input.Add((float)model.Oldpeak);

        // Sex_Male (0 for Female, 1 for Male)
        input.Add(model.Sex == "M" ? 1 : 0);

        // ChestPainType_ASY, ATA, NAP (drop_first=True olduğu için TA yok)
        input.Add(model.ChestPainType == "ASY" ? 1 : 0);
        input.Add(model.ChestPainType == "ATA" ? 1 : 0);
        input.Add(model.ChestPainType == "NAP" ? 1 : 0);

        // RestingECG_LVH, Normal (drop_first=True olduğu için ST yok)
        input.Add(model.RestingECG == "LVH" ? 1 : 0);
        input.Add(model.RestingECG == "Normal" ? 1 : 0);

        // ExerciseAngina_Y (0 for No, 1 for Yes)
        input.Add(model.ExerciseAngina == "Y" ? 1 : 0);

        // ST_Slope_Down, Flat (drop_first=True olduğu için Up yok)
        input.Add(model.ST_Slope == "Down" ? 1 : 0);
        input.Add(model.ST_Slope == "Flat" ? 1 : 0);

        return input;
    }
}
