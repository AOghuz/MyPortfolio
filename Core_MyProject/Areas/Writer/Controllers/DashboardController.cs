using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core_MyProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<WriterUser> _userManager;

        public DashboardController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Find user by name
            var userName = User.Identity?.Name ?? string.Empty; // Ensure we have a valid username
            var values = await _userManager.FindByNameAsync(userName);

            // Check if the user is found
            if (values != null)
            {
                ViewBag.v = $"{values.Name ?? string.Empty} {values.Surname ?? string.Empty}"; // Handle null Name/Surname
            }
            else
            {
                ViewBag.v = "Unknown User"; // Default message if user is not found
            }

            // Weather API
            string api = "dc8f77ff2987cc37bb936ae0a36f6783";
            string connection = $"https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid={api}";

            try
            {
                XDocument document = XDocument.Load(connection);
                var temperature = document.Descendants("temperature").FirstOrDefault();
                ViewBag.v5 = temperature?.Attribute("value")?.Value ?? "No Data"; // Handle null temperature value
            }
            catch (Exception)
            {
                ViewBag.v5 = "Weather data not available"; // Default message on error
            }

            // Statistics
            using (var context = new Context())
            {
                if (!string.IsNullOrEmpty(values?.Email)) // Null kontrolü
                {
                    ViewBag.v1 = context.WriterMessages.Where(x => x.Receiver == values.Email).Count();
                }
                else
                {
                    ViewBag.v1 = 0; // Email null ise varsayılan değer
                }

                ViewBag.v2 = context.Announcements.Count();
                ViewBag.v3 = context.Users.Count();
                ViewBag.v4 = context.Skills.Count();
            }

            return View();
        }

    }
}
// https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid=dc8f77ff2987cc37bb936ae0a36f6783
