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
    public class ServiceManager : IGenericService<Service> // BusinessLayer.Abstract.IServiceService : IGenericService<Service>
    {
        // Concrete klasörü içindeki classları 'Manager' olarak adlandıracağız
        // Abstract klasörü içindekileri 'Service' olarak adlandırmıştık.

        IServiceDal _ServiceDal;
        public ServiceManager(IServiceDal servicedal)
        {
            _ServiceDal = servicedal;// bu eşleştirmeyi yaptıktan sonra IGenericDal metotlarımıza erişebiliriz artık
        }

        public void TAdd(Service t)
        {
            _ServiceDal.Insert(t); // DataAccessLayer da tanımladığımız DataAccessLayer.Abstract.IGenericDal ortak CRUD metotlarımız
        }

        public void TDelete(Service t)
        {
            _ServiceDal.Delete(t);// IGenericDal metotlarımıza erişebiliriz
        }

        public Service TGetById(int id)
        {
            return _ServiceDal.GetById(id);
        }

        public List<Service> TGetList()  // GenericRepository<T> clasımızın metotları ile 
        {
            return _ServiceDal.GetList(); // db den veri çekecek
        }

        public List<Service> TGetListbyFilter()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Service t)
        {
            _ServiceDal.Update(t);
        }
    }
}
