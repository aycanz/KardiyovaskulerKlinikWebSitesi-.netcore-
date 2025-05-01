using BussinessLayer.Abstract;
using DataAccessesLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService <T> where T : class
    {
        GenericRepository<T> repo=new GenericRepository<T>();
        public void TAdd(T entity)
        {
            throw new NotImplementedException();
        }

        public Task TAddAsync(T t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task TDeleteAsync(T t)
        {
            throw new NotImplementedException();
        }

        public List<T> TGetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> TGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> TGetListById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> TGetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public Task TUpdateAsync(T t)
        {
            throw new NotImplementedException();
        }
    }
}
