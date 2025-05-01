using DataAccessesLayer.Abstract;
using DataAccessesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessesLayer.Repositories
{

    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new Context();
            c.Remove(t);
            c.SaveChanges();
        }

        public async Task DeleteAsync(T t)
        {
            using var c = new Context();
            c.Remove(t);
            await c.SaveChangesAsync();
        }

        public T GetById(int id)
        {
            using var c = new Context();
            return c.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var c = new Context();
            return await c.Set<T>().FindAsync(id);
        }


        public List<T> GetListAll()
        {
            using var c = new Context();
            return c.Set<T>().ToList();
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return c.Set<T>().Where(filter).ToList();
        }

        public async Task<List<T>> GetListAllAsync()
        {
            using var c = new Context();
            return await c.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetListAllAsync(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return await c.Set<T>().Where(filter).ToListAsync();
        }

        public void Insert(T t)
        {
            using var c = new Context();
            c.Add(t);
            c.SaveChanges();
        }

        public async Task InsertAsync(T t)
        {
            using var c = new Context();
            await c.AddAsync(t);
            await c.SaveChangesAsync();
        }

        public void Update(T t)
        {
            using var c = new Context();
            c.Update(t);
            c.SaveChanges();
        }

        public async Task UpdateAsync(T t)
        {
            using var c = new Context();
            c.Update(t);
            await c.SaveChangesAsync();
        }
    }
}