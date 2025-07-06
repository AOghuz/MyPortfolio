using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T P);  //void Insert(Skill P); 
        void Update(T P); //void Update(Skill P);
        void Delete(T P); //void Delete(Skill P);
        List<T> GetList(); //List<Skill> GetList();
        T GetById(int id);   //SkillRefName.GetById(3);
        List<T> GetByFilter(Expression<Func<T, bool>> filter);
    }
}
