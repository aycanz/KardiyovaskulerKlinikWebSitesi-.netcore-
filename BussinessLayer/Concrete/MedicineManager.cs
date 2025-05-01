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
    public class MedicineManager : IGenericService<Medicine>
    {
        IMedicineDal _medicineDal;

        public MedicineManager(IMedicineDal medicineDal)
        {
            _medicineDal = medicineDal;
        }

        public void TAdd(Medicine t)
        {
            _medicineDal.Insert(t);

         }

        public Task TAddAsync(Medicine t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Medicine t)
        {
            _medicineDal.Delete(t);
         }

        public Task TDeleteAsync(Medicine t)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> TGetAll()
        {
            return _medicineDal.GetListAll();
         }

        public Task<List<Medicine>> TGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Medicine TGetById(int id)
        {
 var medicine = _medicineDal.GetById(id);
            return _medicineDal.GetById(id);
                }

        public Task<Medicine> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> TGetListById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Medicine>> TGetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Medicine t)
        {
            _medicineDal.GetListAll();
         }

        public Task TUpdateAsync(Medicine t)
        {
            throw new NotImplementedException();
        }
    }
}
