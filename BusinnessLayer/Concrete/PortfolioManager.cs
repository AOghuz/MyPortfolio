using BusinnessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    // Manager : BL to DAL için aracımızdır. 
    // bu class normalde böyle değil AboutManagere bak
    public class PortfolioManager : IPortfolioService
    {
        EfPortfolioDal _efPortfolioDal = new EfPortfolioDal();

        public PortfolioManager(EfPortfolioDal efPortfolioDal)
        {
            _efPortfolioDal = efPortfolioDal;
        }

        public void TAdd(Portfolio t)
        {
            _efPortfolioDal.Insert(t);
        }

        public void TDelete(Portfolio t)
        {
            _efPortfolioDal.Delete(t);
        }

        public Portfolio TGetById(int id)
        {
            return _efPortfolioDal.GetById(id);
        }

        public List<Portfolio> TGetList()
        {
            return _efPortfolioDal.GetList();
        }

        public List<Portfolio> TGetListbyFilter()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Portfolio t)
        {
            _efPortfolioDal.Update(t);
        }
    }
}
