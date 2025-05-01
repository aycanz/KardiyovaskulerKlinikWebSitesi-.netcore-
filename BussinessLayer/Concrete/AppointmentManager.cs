using BussinessLayer.Abstract;
using DataAccessesLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class AppointmentManager : IGenericService<Appointment>
    {
        IAppointmentDal _appointmentDal;

        public AppointmentManager(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public void TAdd(Appointment t)
        {
            _appointmentDal.Insert(t);
        }

        public async Task TAddAsync(Appointment t)
        {
            await _appointmentDal.InsertAsync(t);
        }

        public void TDelete(Appointment t)
        {
            _appointmentDal.Delete(t);
        }

        public async Task TDeleteAsync(Appointment t)
        {
            await _appointmentDal.DeleteAsync(t);
        }

        public List<Appointment> TGetAll()
        {
            return _appointmentDal.GetAppointmentsWithDetails(); // 🔹 Include edilmiş veriyi getir
        }

        public async Task<List<Appointment>> TGetAllAsync()
        {
            return await _appointmentDal.GetAppointmentsWithDetailsAsync();
        }
        public Appointment TGetById(int id)
        {
            var appointment = _appointmentDal.GetById(id);
            return _appointmentDal.GetById(id);
        }

        public async Task<Appointment> TGetByIdAsync(int id)
        {
            return await _appointmentDal.GetByIdAsync(id);
        }

        public List<Appointment> TGetListById(int id)
        {
            return _appointmentDal.GetListAll(x => x.PatientId == id);
        }

        public async Task<List<Appointment>> TGetListByIdAsync(int id)
        {
            return await _appointmentDal.GetListAllAsync(x => x.PatientId == id);
        }

        public void TUpdate(Appointment t)
        {
            _appointmentDal.Update(t);
        }

        public Task TUpdateAsync(Appointment t)
        {
            throw new NotImplementedException();
        }
    }
}
