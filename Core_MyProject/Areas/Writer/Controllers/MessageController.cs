using BusinnessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core_MyProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/Message")]
    [Authorize]
    public class MessageController : Controller
    {
        WriterMessageManager writerMessageManager = new WriterMessageManager(new EfWriterMessageDal());
        private readonly UserManager<WriterUser> _userManager;

        public MessageController(UserManager<WriterUser> userManager)
        {
            _userManager = userManager;
        }

        // Rota: Writer/Message/ReceiverMessage
        [Route("ReceiverMessage")]
        public async Task<IActionResult> ReceiverMessage(string p)
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return View("Error");
            }

            var values = await _userManager.FindByNameAsync(userName);

            if (values?.Email != null)
            {
                p = values.Email;
            }
            else
            {
                return View("Error");
            }

            var messageList = writerMessageManager.GetListReceiverMessage(p);
            return View(messageList);
        }

        // Rota: Writer/Message/SenderMessage
        [Route("SenderMessage")]
        public async Task<IActionResult> SenderMessage(string p)
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return View("Error");
            }

            var values = await _userManager.FindByNameAsync(userName);

            if (values?.Email != null)
            {
                p = values.Email;
            }
            else
            {
                return View("Error");
            }

            var messageList = writerMessageManager.GetListSenderMessage(p);
            return View(messageList);
        }

        // Rota: Writer/Message/MessageDetails/{id}
        [Route("MessageDetails/{id}")]
        public IActionResult MessageDetails(int id)
        {
            WriterMessage writerMessage = writerMessageManager.TGetById(id);
            return View(writerMessage);
        }

        // Rota: Writer/Message/ReceiverMessageDetails/{id}
        [Route("ReceiverMessageDetails/{id}")]
        public IActionResult ReceiverMessageDetails(int id)
        {
            WriterMessage writerMessage = writerMessageManager.TGetById(id);
            return View(writerMessage);
        }

        // Rota: Writer/Message/SendMessage (GET)
        [HttpGet]
        [Route("SendMessage")]
        public IActionResult SendMessage()
        {
            return View();
        }

        // Rota: Writer/Message/SendMessage (POST)
        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(WriterMessage p)
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return View("Error");
            }

            var values = await _userManager.FindByNameAsync(userName);

            if (values == null || string.IsNullOrEmpty(values.Email) || string.IsNullOrEmpty(values.Name) || string.IsNullOrEmpty(values.Surname))
            {
                return View("Error");
            }

            string mail = values.Email;
            string name = values.Name + " " + values.Surname;

            p.Date = DateTime.Now;
            p.Sender = mail;
            p.SenderName = name;

            using (Context c = new Context())
            {
                var usernamesurname = c.Users
                    .Where(x => x.Email == p.Receiver)
                    .Select(y => y.Name + " " + y.Surname)
                    .FirstOrDefault();

                if (usernamesurname == null)
                {
                    return View("Error");
                }

                p.ReceiverName = usernamesurname;
            }

            writerMessageManager.TAdd(p);
            return RedirectToAction("SenderMessage");
        }
    }
}
