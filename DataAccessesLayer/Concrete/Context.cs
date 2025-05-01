using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessesLayer.Concrete
{
    public class Context :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = AYCAN; Initial Catalog = Db_KVH; Integrated Security = True;TrustServerCertificate=True");
        }

        public DbSet<Doctor>Doctors{ get; set; }
        public DbSet <Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Appointment> Appointments { get; set;}
        public DbSet<HealthJournal> HealthJournals { get; set; }
        public DbSet<Result>  Results { get; set; }





    }
}
