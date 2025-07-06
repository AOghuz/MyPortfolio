
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core_MyProject.ViewComponents.Contact
{
    public class SendMessage : ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());

        [HttpGet]
        public IViewComponentResult Invoke()
        {
            return View();
        }

        //[HttpPost]
        //public IViewComponentResult Invoke(Message P)
        //{
        //    P.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString()); // Mesajın kaydedildiği tarihi DB ye yaz
        //    P.Status = true; // Okunduktan sonra false yapacaz
        //    messageManager.TAdd(P);
        //    return View();
        //}

    }
}
