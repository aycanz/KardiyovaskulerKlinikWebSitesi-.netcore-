using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessesLayer.Abstract
{
    public interface IAppointmentDal:IGenericDal<Appointment>
    {
        List<Appointment> GetAppointmentsWithDetails();
        Task InsertAsync(Appointment t);
        Task<List<Appointment>> GetAppointmentsWithDetailsAsync();

    }
}
