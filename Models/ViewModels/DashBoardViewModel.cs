
using EntityLayer.Concrete;

namespace KVH.Models.ViewModels
{
    public class DashBoardViewModel
    {
        public Patient Patient { get; set; }  // Hasta bilgileri
        public HealthJournal HealthJournal { get; set; }  // Sağlık metrikleri
        public List<Appointment> Appointments { get; set; }  // Randevular
        public Appointment? UpcomingAppointment { get; internal set; }
    }
}
