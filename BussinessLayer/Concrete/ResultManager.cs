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
    public class ResultManager : IGenericService<Result>
    {
        IResultDal _resultDal;

        public ResultManager(IResultDal resultDal)
        {
            _resultDal = resultDal;
        }

        public void TAdd(Result t)
        {
         _resultDal.Insert(t);
                }

        public Task TAddAsync(Result t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Result t)
        {
            _resultDal.Delete(t);
 
        }

        public Task TDeleteAsync(Result t)
        {
            throw new NotImplementedException();
        }

        public List<Result> TGetAll()
        {
          return  _resultDal.GetListAll();


        }

        public Task<List<Result>> TGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Result TGetById(int id)
        {
           var result= _resultDal.GetById(id);
            return _resultDal.GetById(id);
        }

        public Task<Result> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Result> TGetListById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Result>> TGetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Result t)
        {
           _resultDal.Update(t);
        }

        public Task TUpdateAsync(Result t)
        {
            throw new NotImplementedException();
        }
    }
}
