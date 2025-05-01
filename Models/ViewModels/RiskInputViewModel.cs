namespace KVH.Models.ViewModels
{
    public class RiskInputViewModel
    {
        public int Age { get; set; }
        public string Sex { get; set; }  // "M" veya "F"
        public string ChestPainType { get; set; }
        public int RestingBP { get; set; }
        public int Cholesterol { get; set; }
        public int FastingBS { get; set; }
        public string RestingECG { get; set; }
        public int MaxHR { get; set; }
        public string ExerciseAngina { get; set; }  // "Y" veya "N"
        public double Oldpeak { get; set; }
        public string ST_Slope { get; set; }
    }
}