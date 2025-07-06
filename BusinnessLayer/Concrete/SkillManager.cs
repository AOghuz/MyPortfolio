using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SkillManager : ISkillService
    {
        ISkillDal _Skilldal; // EfSkillDal atanacak üzerinden GenericRepository matotlarına erişeceğiz

        public SkillManager(ISkillDal skillDal) // EfSkillDal gelecek
        {
            _Skilldal = skillDal; //= EfSkillDal
        }

        public void TAdd(Skill t)
        {
            _Skilldal.Insert(t);
        }

        public void TDelete(Skill t)
        {
            _Skilldal.Delete(t);
        }

        public Skill TGetById(int id)
        {
            return _Skilldal.GetById(id);
        }

        public List<Skill> TGetList()
        {
            return _Skilldal.GetList();
        }

        public List<Skill> TGetListbyFilter()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Skill t)
        {
            _Skilldal.Update(t);
        }
    }
}
