using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core_Proje.ViewComponents
{
    public class FeatureList : ViewComponent
    {

        FeatureManager featureManager = new FeatureManager(new EfFeatureDal()); // EfFeatureDal : GenericRepository<Feature>, IFeatureDal
        public IViewComponentResult Invoke()
        {
            var values = featureManager.TGetList();
            return View(values);
        }


        //public IActionResult Index()  
        //{
        //    return View();
        //}
    }
}