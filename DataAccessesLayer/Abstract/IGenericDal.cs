using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessesLayer.Abstract
{
        public interface IGenericDal<T> where T : class
        {
            void Insert(T t);
            void Delete(T t);
            void Update(T t);
            List<T> GetListAll();
        List<T> GetListAll(Expression<Func<T,bool>> filter);


        T GetById(int id);

        Task InsertAsync(T t);
        Task DeleteAsync(T t);
        Task UpdateAsync(T t);
        Task<List<T>> GetListAllAsync();
        Task<List<T>> GetListAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(int id);

    }
    }

