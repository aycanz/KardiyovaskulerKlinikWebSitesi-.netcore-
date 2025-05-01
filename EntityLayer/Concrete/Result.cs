using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Result
    {


        [Key]
        public int ResultId { get; set; }
        public int AppointmentId { get; set; }

        public string ChestPainType { get; set; }
        public int RestingBP { get; set; }
        public int Cholesterol { get; set; }
        public int FastingBS { get; set; }
        public string RestingECG { get; set; }
        public int MaxHR { get; set; }
        public string ExerciseAngina { get; set; }
        public double Oldpeak { get; set; }
        public string ST_Slope { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }


        /*  [Key] 
          public int SonucId { get; set; }
          public int RandevuId { get; set; }
          public int HastaId { get; set; }
          public string ChestPainType { get; set; }
          public int RestingBP { get; set; }
          public int Cholesterol { get; set; }
          public int FastingBS { get; set; }
          public string RestingECG { get; set; }
          public int MaxHR { get; set; }
          public string ExerciseAngina { get; set; }
          public double Oldpeak { get; set; }
          public string ST_Slope { get; set; }

          [ForeignKey("RandevuId")]
          public Appointment Randevu { get; set; }
          [ForeignKey("HastaId")]
          public Patient Hasta { get; set; }*/
    }
}
