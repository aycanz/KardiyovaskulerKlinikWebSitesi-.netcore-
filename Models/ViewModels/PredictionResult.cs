namespace KVH.Models.ViewModels
{
    public class PredictionResult
    {
        public bool Risk { get; set; }  // 0 veya 1
        public float Probability { get; set; }  // Tahmin olasılığı (örn. 0.85)
    }

}
