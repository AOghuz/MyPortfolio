using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Core_MyProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {
        SkillManager skillManager = new SkillManager(new EfSkillDal());
        public IActionResult Index()
        {
            ViewBag.Url1 = "Yetenekler";
            ViewBag.Url2 = "Skill";

            var values = skillManager.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddSkill() // Yeni Yetenek Ekle - Get
        {
            ViewBag.Url1 = "Yetenek Ekle";
            ViewBag.Url2 = "Skill";
            ViewBag.Url3 = "AddSkill";
            return View();
        }

        [HttpPost]
        public IActionResult AddSkill(Skill skill) // Yeni Yetenek Ekle - Post
        {
            skillManager.TAdd(skill);
            return RedirectToAction("Index");
        }

        [HttpGet] // bunu yazmasan da olur varsayılanı HttpGet zaten, ama sen yaz
        public IActionResult DeleteSkill(int id)
        {
            var values = skillManager.TGetById(id);
            skillManager.TDelete(values);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditSkill(int id) // Yetenek Güncelle - GET(urlden)
        {
            ViewBag.Url1 = "Yetenek Güncelleme";
            ViewBag.Url2 = "Skill";
            ViewBag.Url3 = "EditSkill";
            Skill values = skillManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult EditSkill(Skill skill) // Yetenek Güncelle - Post(formdan)
        {
            skillManager.TUpdate(skill);
            return RedirectToAction("Index");
        }

    }
}