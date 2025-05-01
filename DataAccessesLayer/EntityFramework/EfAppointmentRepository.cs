using DataAccessesLayer.Abstract;
using DataAccessesLayer.Concrete;
using DataAccessesLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessesLayer.EntityFramework
{
    public class EfAppointmentRepository : GenericRepository<Appointment>, IAppointmentDal
    {
        public List<Appointment> GetAppointmentsWithDetails()
        {
            using var c = new Context();

            return c.Appointments
                            .Include(a => a.Patient)  // 🔹 Patient tablosunu getir
                            .Include(a => a.Doctor)   // 🔹 Doctor tablosunu getir
                            .ToList();
        }

        public async Task<List<Appointment>> GetAppointmentsWithDetailsAsync()
        {
            using var c = new Context();
            return await c.Appointments
                            .Include(a => a.Patient)  // 🔹 Patient tablosunu getir
                            .Include(a => a.Doctor)   // 🔹 Doctor tablosunu getir
                            .ToListAsync();          // Asenkron işlem için ToListAsync kullanılıyor
        }

        public List<Appointment> TGetListById(int patientId)
        {
            using var c = new Context();// Kendi DbContext'ini kullan
            
                return c.Appointments
                              .Include(a => a.Doctor)  // 🔹 Doctor ilişkisini dahil et
                              .Where(a => a.PatientId == patientId)
                              .ToList();
            
        }
        public async Task InsertAsync(Appointment t)
        {
            using var c = new Context();
            await c.Appointments.AddAsync(t);  // Asenkron ekleme işlemi
            await c.SaveChangesAsync();       // Asenkron olarak değişiklikleri kaydetme
        }
    }
}
