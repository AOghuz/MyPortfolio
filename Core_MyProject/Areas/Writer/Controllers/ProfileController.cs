using Core_MyProject.Areas.Writer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core_MyProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public ProfileController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User?.Identity?.Name ?? string.Empty);
            if (values == null)
            {
                return NotFound(); // Handle the case where the user is not found.
            }

            UserEditViewModel model = new UserEditViewModel
            {
                Name = values.Name ?? string.Empty, // Using null-coalescing operator to avoid null
                Surname = values.Surname ?? string.Empty,
                PictureURL = values.ImageURL ?? string.Empty
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name ?? string.Empty);
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamadığında durumu yönet.
            }

            if (p.Picture != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Picture.FileName); // Picture null değil, bu yüzden direkt erişilebilir.
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(resource, "wwwroot", "userimage", imageName);

                // `p.Picture`'ın null olmadığını bildiğimiz için burada `await` ile kullanılabilir.
                await using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await p.Picture.CopyToAsync(stream);
                }
                user.ImageURL = imageName;
            }

            user.Name = p.Name ?? string.Empty;
            user.Surname = p.Surname ?? string.Empty;

            if (!string.IsNullOrEmpty(p.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.Password);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

    }
}
