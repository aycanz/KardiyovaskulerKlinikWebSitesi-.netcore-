using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TAdd(T t);
        void TDelete(T t);
        void TUpdate(T t);
        List<T> TGetAll();
        List<T> TGetListById(int id);
        T TGetById(int id);
        Task TAddAsync(T t);
        Task TDeleteAsync(T t);
        Task TUpdateAsync(T t);
        Task<List<T>> TGetAllAsync();
        Task<List<T>> TGetListByIdAsync(int id);
        Task<T> TGetByIdAsync(int id);

    }
}
