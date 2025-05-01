using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class HealthJournal
    {
        [Key]
        public int HealthJournalId { get; set; }

        // Hasta ile ilişki
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; } // Nullable yapıldı.

        // Günlük sağlık verileri
        public DateTime Date { get; set; }
        public string BloodPressure { get; set; } // Örnek: "120/80"
        public int BloodSugar { get; set; } // Şeker
        public int HeartRate { get; set; } // Nabız
    }

}
