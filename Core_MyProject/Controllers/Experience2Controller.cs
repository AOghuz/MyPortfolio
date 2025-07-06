using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core_MyProject.Controllers
{
    public class Experience2Controller : Controller
    {
        ExperienceManager experienceManager = new ExperienceManager(new EfExperienceDal());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListExperience()
        {
            var values = JsonConvert.SerializeObject(experienceManager.TGetList());
            return Json(values);
        }
        [HttpPost]
        public IActionResult AddExperience(Experience p)
        {
            experienceManager.TAdd(p);
            var values = JsonConvert.SerializeObject(p);
            return Json(values);
        }
        public IActionResult GetById(int ExperienceId)
        {
            var v = experienceManager.TGetById(ExperienceId);
            var values = JsonConvert.SerializeObject(v);
           return Json(values);
        }
        
        public IActionResult DeleteExperience(int id)
        {
            var v = experienceManager.TGetById(id);
            experienceManager.TDelete(v);
            return NoContent();
        }

        [HttpPost]
        public IActionResult UpdateExperince(Experience p)
        {
            if (p.ExperienceId == null)
            {
                return BadRequest("ExperienceId is null.");
            }

            // ID'ye göre mevcut kaydı veri tabanından alıyoruz
            var v = experienceManager.TGetById(p.ExperienceId.Value);

            if (v == null)
            {
                return NotFound("Experience not found.");
            }

            // Gelen yeni değerlerle alanları güncelliyoruz
            v.Name = p.Name;
            v.Date = p.Date;

            // Güncellenmiş veriyi veri tabanına kaydediyoruz
            experienceManager.TUpdate(v);

            return Json(v); // Güncellenmiş veriyi geri döndürüyoruz
        }



    }
}
