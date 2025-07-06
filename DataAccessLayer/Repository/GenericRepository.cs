using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new Context();
            c.Remove(t);
            c.SaveChanges();
        }

        public List<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return c.Set<T>().Where(filter).ToList();
        }

        public T GetById(int id)
        {
            using var c = new Context();
            return c.Set<T>().Find(id)!; // null olamayacağı varsayılıyor
        }


        public List<T> GetList()
        {
            using var c = new Context();
            return c.Set<T>().ToList(); // ToList: EF metodu
        }

        public void Insert(T t)
        {
            using var c = new Context();
            c.Add(t); // AddList: EF metodu
            c.SaveChanges();
        }

        public void Update(T t)
        {
            using var c = new Context();
            c.Update(t); // Update: EF metodu : .net core ile geldi
            c.SaveChanges();
        }
    }
}
