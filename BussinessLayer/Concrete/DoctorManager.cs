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
    public class DoctorManager : IGenericService<Doctor>
    {
        IDoctorDal _doctorDal;

        public DoctorManager(IDoctorDal doctorDal)
        {
            _doctorDal = doctorDal;
        }

        public void TAdd(Doctor t)
        {
            _doctorDal.Insert(t);
        }

        public async Task TAddAsync(Doctor t)
        {
            await _doctorDal.InsertAsync(t);
        }

        public void TDelete(Doctor t)
        {
            _doctorDal.Delete(t);
        }

        public async Task TDeleteAsync(Doctor t)
        {
            await _doctorDal.DeleteAsync(t);
        }

        public List<Doctor> TGetAll()
        {
            return _doctorDal.GetListAll();


        }

        public async Task<List<Doctor>> TGetAllAsync()
        {
            return await _doctorDal.GetListAllAsync();
        }

        public Doctor TGetById(int id)
        {
            var doctor = _doctorDal.GetById(id);
            return _doctorDal.GetById(id);
        }

        public async Task<Doctor> TGetByIdAsync(int id)
        {
            return _doctorDal.GetById(id);
        }

        public List<Doctor> TGetListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Doctor>> TGetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Doctor t)
        {
            _doctorDal.Update(t);
        }

        public async Task TUpdateAsync(Doctor t)
        {
            await _doctorDal.UpdateAsync(t);
        }
    }
}