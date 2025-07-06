using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyProject.Areas.Writer.ViewComponents
{
    public class Navbar1 : ViewComponent
    {
        private readonly UserManager<WriterUser> _userManager;

        public Navbar1(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                // User.Identity.Name null veya boşsa, bir alternatif davranış sergileyebilirsin.
                ViewBag.v = null;
                return View();
            }

            var values = await _userManager.FindByNameAsync(userName);

            // values da nullable olabilir, bu yüzden kontrol ediyoruz
            ViewBag.v = values?.ImageURL;

            return View();
        }

    }
}
