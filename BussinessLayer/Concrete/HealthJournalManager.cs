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
    public class HealthJournalManager : IGenericService<HealthJournal>
    {
        IHealthJournalDal _healthJournalDal;

        public HealthJournalManager(IHealthJournalDal healthJournalDal)
        {
            _healthJournalDal = healthJournalDal;
        }

        public void TAdd(HealthJournal t)
        {
            _healthJournalDal.Insert(t);
        }

        public async Task TAddAsync(HealthJournal t)
        {
            await _healthJournalDal.InsertAsync(t);  // Asenkron insert işlemi
         }

        public void TDelete(HealthJournal t)
        {
            _healthJournalDal.Delete(t);
        }

        public async Task TDeleteAsync(HealthJournal t)
        {
            await _healthJournalDal.DeleteAsync(t);  // Asenkron delete işlemi
        }

        public List<HealthJournal> TGetAll()
        {
            return _healthJournalDal.GetListAll();
         }

        public async Task<List<HealthJournal>> TGetAllAsync()
        {
            return await _healthJournalDal.GetListAllAsync();  // Asenkron GetListAll işlemi
        }

        public HealthJournal TGetById(int id)
        {
          
            return _healthJournalDal.GetById(id);
        }

        public async Task<HealthJournal> TGetByIdAsync(int id)
        {
            return await _healthJournalDal.GetByIdAsync(id);  // Asenkron GetById işlemi
        }

        public List<HealthJournal> TGetListById(int id)
        {
            return _healthJournalDal.GetListAll(h => h.PatientId == id); // PatientId'ye göre filtreleme
        }

        public async Task<List<HealthJournal>> TGetListByIdAsync(int id)
        {
            return await _healthJournalDal.GetListAllAsync(h => h.PatientId == id);  // Asenkron olarak PatientId'ye göre filtreleme
        }

        public void TUpdate(HealthJournal t)
        {
            _healthJournalDal.Update(t);
         }

        public async Task TUpdateAsync(HealthJournal t)
        {
            await _healthJournalDal.UpdateAsync(t);
        }
    }
}
