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
    public class PatientManager : IGenericService<Patient>
    {

        IPatientDal _patientDal;

        public PatientManager(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        public void TAdd(Patient t)
        {
            _patientDal.Insert(t);
        }

        public async Task TAddAsync(Patient t)
        {
            await _patientDal.InsertAsync(t); 
        }

        public void TDelete(Patient t)
        {
            _patientDal.Delete(t);
        }

        public async Task TDeleteAsync(Patient t)
        {
            await _patientDal.DeleteAsync(t); 
        }
        public List<Patient> TGetAll()
        {
         return _patientDal.GetListAll();
        }

        public async Task<List<Patient>> TGetAllAsync()
        {
            return await _patientDal.GetListAllAsync(); 
        }

        public Patient TGetById(int id)
        {
         var patient = _patientDal.GetById(id);
            return _patientDal.GetById(id);
        }

        public async Task<Patient> TGetByIdAsync(int id)
        {
            return await _patientDal.GetByIdAsync(id);  
        }

        public List<Patient> TGetListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Patient>> TGetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Patient t)
        {
            _patientDal.Update(t);
         }

        public async Task TUpdateAsync(Patient t)
        {
            await _patientDal.UpdateAsync(t);  
        }
    }
}
