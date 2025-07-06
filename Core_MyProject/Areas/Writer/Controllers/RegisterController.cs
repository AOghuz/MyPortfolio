using Core_MyProject.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core_MyProject.Areas.Writer.Controllers
{
    [AllowAnonymous]
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    public class RegisterController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public RegisterController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel p)
        {
            // Gerekli alanların dolu olduğundan emin olun.
            if (string.IsNullOrEmpty(p.Name) ||
                string.IsNullOrEmpty(p.Surname) ||
                string.IsNullOrEmpty(p.Mail) ||
                string.IsNullOrEmpty(p.UserName) ||
                string.IsNullOrEmpty(p.Password) ||
                string.IsNullOrEmpty(p.ConfirmPassword))
            {
                ModelState.AddModelError("", "Lütfen tüm alanları doldurun.");
                return View(p); // Geri dön ve formu doldurmasını iste
            }

            // Password ve ConfirmPassword eşleşiyorsa
            if (p.Password == p.ConfirmPassword)
            {
                WriterUser w = new WriterUser()
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    Email = p.Mail,
                    UserName = p.UserName,
                    ImageURL = p.ImageURL
                };

                var result = await _userManager.CreateAsync(w, p.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor.");
            }

            return View(p);
        }
    }
}
