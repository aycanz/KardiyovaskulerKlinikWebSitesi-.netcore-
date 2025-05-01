using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class RiskPredictionService
{
    private readonly HttpClient _httpClient;

    public RiskPredictionService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<float> PredictRisk(float[] inputData)
    {
        // Flask API'ye gönderilecek veri
        var data = new { input = inputData };

        // JSON veriye çevir
        var jsonData = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        // Flask API URL
        var url = "http://127.0.0.1:5000/predict";

        // API'ye istek gönder
        var response = await _httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            // Gelen cevabı JArray olarak parse et
            var json = JObject.Parse(responseString);
            var predictionArray = json["prediction"] as JArray;

            // İlk sonucu döndür (0 veya 1 gelir)
            return predictionArray != null ? predictionArray[0].ToObject<float>() : throw new Exception("Tahmin alınamadı.");
        }

        throw new Exception("API isteği başarısız oldu.");
    }
}
